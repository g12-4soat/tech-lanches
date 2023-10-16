﻿using System.Text.RegularExpressions;
using TechLanches.Core;

namespace TechLanches.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private Email()
        {

        }

        public Email(string email)
        {
            ArgumentNullException.ThrowIfNull(email);

            EnderecoEmail = Validar(email) ? email : throw new DomainException($"Email inválido {email}");
        }

        public string EnderecoEmail { get; private set; }

        public static bool Validar(string email)
        {
            const string regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,6}$";

            return Regex.IsMatch(email, regex);
        }

        protected override IEnumerable<object> RetornarPropriedadesDeEquidade()
        {
            yield return EnderecoEmail;
        }
    }
}