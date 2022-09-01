using System.Collections.Generic;
using System.Linq;

namespace Movian.Business.Validations.DocumentValidations
{
  public class CpfValidation
  {
    public const int TamanhoCpf = 11;

    public static bool Validate(string cpf)
    {
      var cpfNumeros = Utils.JustNumbers(cpf);

      if (!ValidSize(cpfNumeros)) return false;
      return !HasDigitValidator(cpfNumeros) && HasValidDigits(cpfNumeros);
    }

    private static bool ValidSize(string valor)
    {
      return valor.Length == TamanhoCpf;
    }

    private static bool HasDigitValidator(string valor)
    {
      string[] invalidNumbers =
      {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
      return invalidNumbers.Contains(valor);
    }

    private static bool HasValidDigits(string valor)
    {
      var number = valor.Substring(0, TamanhoCpf - 2);
      var digitoValidation = new DigitoValidation(number)
          .WithMultiplierUntil(2, 11)
          .Replacing("0", 10, 11);
      var firstDigit = digitoValidation.CalculateDigit();
      digitoValidation.AddDigit(firstDigit);
      var secondDigit = digitoValidation.CalculateDigit();

      return string.Concat(firstDigit, secondDigit) == valor.Substring(TamanhoCpf - 2, 2);
    }
  }

  public class CnpjValidation
  {
    public const int TamanhoCnpj = 14;

    public static bool Validate(string cpnj)
    {
      var cnpjNumeros = Utils.JustNumbers(cpnj);

      if (!HasValidSize(cnpjNumeros)) return false;
      return !HasDigitValidator(cnpjNumeros) && HasValidDigits(cnpjNumeros);
    }

    private static bool HasValidSize(string valor)
    {
      return valor.Length == TamanhoCnpj;
    }

    private static bool HasDigitValidator(string valor)
    {
      string[] invalidNumbers =
      {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
      return invalidNumbers.Contains(valor);
    }

    private static bool HasValidDigits(string valor)
    {
      var number = valor.Substring(0, TamanhoCnpj - 2);

      var digitoValidation = new DigitoValidation(number)
          .WithMultiplierUntil(2, 9)
          .Replacing("0", 10, 11);
      var firstDigit = digitoValidation.CalculateDigit();
      digitoValidation.AddDigit(firstDigit);
      var secondDigit = digitoValidation.CalculateDigit();

      return string.Concat(firstDigit, secondDigit) == valor.Substring(TamanhoCnpj - 2, 2);
    }
  }

  public class DigitoValidation
  {
    private string _number;
    private const int Module = 11;
    private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
    private readonly IDictionary<int, string> _substituicoes = new Dictionary<int, string>();
    private bool _complementarDoModule = true;

    public DigitoValidation(string numero)
    {
      _number = numero;
    }

    public DigitoValidation WithMultiplierUntil(int primeiroMultiplicador, int ultimoMultiplicador)
    {
      _multipliers.Clear();
      for (var i = primeiroMultiplicador; i <= ultimoMultiplicador; i++)
        _multipliers.Add(i);

      return this;
    }

    public DigitoValidation Replacing(string substituto, params int[] digitos)
    {
      foreach (var i in digitos)
      {
        _substituicoes[i] = substituto;
      }
      return this;
    }

    public void AddDigit(string digito)
    {
      _number = string.Concat(_number, digito);
    }

    public string CalculateDigit()
    {
      return !(_number.Length > 0) ? "" : GetDigitSum();
    }

    private string GetDigitSum()
    {
      var soma = 0;
      for (int i = _number.Length - 1, m = 0; i >= 0; i--)
      {
        var produto = (int)char.GetNumericValue(_number[i]) * _multipliers[m];
        soma += produto;

        if (++m >= _multipliers.Count) m = 0;
      }

      var mod = (soma % Module);
      var resultado = _complementarDoModule ? Module - mod : mod;

      return _substituicoes.ContainsKey(resultado) ? _substituicoes[resultado] : resultado.ToString();
    }
  }

  public class Utils
  {
    public static string JustNumbers(string valor)
    {
      var onlyNumber = "";
      foreach (var s in valor)
      {
        if (char.IsDigit(s))
        {
          onlyNumber += s;
        }
      }
      return onlyNumber.Trim();
    }
  }
}