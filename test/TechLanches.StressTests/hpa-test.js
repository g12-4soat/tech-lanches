import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  duration: '100s',
  vus: 300,
};

export default function () {
  let res = http.get('http://localhost:5050/api/pedidos');
  check(res, { 'status 200 OK': (r) => r.status === 200 });
  sleep(1);
}