﻿using System.Text.RegularExpressions;
using TechLanches.Core;

namespace TechLanches.Domain.ValueObjects
{
    public class Cpf: ValueObject
    {
        private Cpf()
        {

        }

        public Cpf(string cpf)
        {
            ArgumentNullException.ThrowIfNull(cpf);

            Numero = Validar(cpf) ? cpf : throw new DomainException($"CPF inválido {cpf}");
        }

        public string Numero { get; private set; }

        public static bool Validar(string cpf)
        {
            // Remove caracteres não numéricos
            string cleanCpf = Regex.Replace(cpf, @"[^\d]", "");

            // Verifica se o CPF tem 11 dígitos
            if (cleanCpf.Length != 11)
            {
                return false;
            }

            // Calcula o primeiro dígito verificador
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(cleanCpf[i].ToString()) * (10 - i);
            }
            int remainder = sum % 11;
            int firstDigit = (remainder < 2) ? 0 : 11 - remainder;

            // Verifica o primeiro dígito verificador
            if (int.Parse(cleanCpf[9].ToString()) != firstDigit)
            {
                return false;
            }

            // Calcula o segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(cleanCpf[i].ToString()) * (11 - i);
            }
            remainder = sum % 11;
            int secondDigit = (remainder < 2) ? 0 : 11 - remainder;

            // Verifica o segundo dígito verificador
            return int.Parse(cleanCpf[10].ToString()) == secondDigit;
        }

        protected override IEnumerable<object> RetornarPropriedadesDeEquidade()
        {
            yield return Numero;
        }
    }
}