import http from 'k6/http';
import { check, sleep } from 'k6';

// export const options = {
//   duration: '1s',
//   vus: 1,
// };

export const options = {
  stages: [
    { target: 800, duration: '30s' },
    { target: 0, duration: '30s' },
  ],
};

export default function () {
  const data = {
    itensPedido: [
      {
        idProduto: 1,
        quantidade: 2
      }
    ]
  }
  const header = { headers: { 'Content-Type': 'application/json' } };
  let pedidoCriadoResponse = http.post('http://localhost:5050/api/pedidos', JSON.stringify(data), header);
  check(pedidoCriadoResponse, { 'status 200 OK': (r) => r.status === 200 });

  const pedido = pedidoCriadoResponse.json();
  const STATUS_PEDIDO_RECEBIDO = 2; 
  let pedidoRecebidoResponse =  http.put(`http://localhost:5050/api/pedidos/${pedido.id}/trocarstatus`, JSON.stringify(parseInt(STATUS_PEDIDO_RECEBIDO)), header);
  check(pedidoRecebidoResponse, { 'status 200 OK': (r) => r.status === 200 });
}
