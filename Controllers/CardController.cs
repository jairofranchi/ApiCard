using System.Net;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace ApiCard.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase {
        private readonly CardContext _context;

        public CardController (CardContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> PostCardInfo (Card card)
        {
            int[] tokenArrayValidado = null;

            string dataAtual = DateTime.UtcNow.ToString("yyyyMMddHHmm");
            card.DateNowUtc = dataAtual;

            card.Token = card.CardNumber.ToString() + dataAtual;

            string jsonResult = "";

            tokenArrayValidado = TokenValidation(card.Token);

            for (int i = 0; i < card.CVV; i++)
            {
                tokenArrayValidado = CircularRotation(tokenArrayValidado);
            }

            card.Token = string.Join("", tokenArrayValidado);

            try
            {
                //SALVA AS INFORMAÇÕES NO BANCO DE DADOS
                _context.Card.Add (card);
                await _context.SaveChangesAsync ();
            }
            catch(Exception ex)
            {
                jsonResult = "{\"Message\":\"Cartão já inserido anteriormente!\"}";

                Console.WriteLine("Erro - " + ex.Message);
                return NotFound(jsonResult);
            }
            
            jsonResult = "{\"Token\":\"" + card.Token + "\", \"Registration Date\":\""+ dataAtual + "\"}";

            return  Ok(jsonResult);
        }


        //*******************************************
        //MÉTODO PARA FAZER A VALIDAÇÃO DO TOKEN
        //ONDE A DIFERENÇA ENTRE DOIS NUMEROS
        //SEJA MENOR OU IGUAL A 4
        //*******************************************
        public int[] TokenValidation(string token)
        {
            string[] str_arr = token.Split("").ToArray();
            int numeroDiferente = 0;
            var arrayInt = token.Select(c => c - '0').ToArray();

            bool foundDiff = false;

            //Ordena o Array de Inteiros
            Array.Sort(arrayInt);

            for (int j = 0; j < arrayInt.Length; j++)
            {
                for (int i = 1; i < arrayInt.Length; i++)
                {
                    if (arrayInt[i] - arrayInt[j] <= 4)
                    {
                        continue;
                    }
                    else
                    {
                        //Caso a diferença seja maior que 4, número é armazenado pois todos os numeros
                        //iguais ou acima desse número, deverão ser removidos da lista posteriormente
                        numeroDiferente = arrayInt[i];
                        foundDiff = true;
                        break;
                    }
                }
                if (foundDiff)
                    break;
            }

            arrayInt = token.Select(c => c - '0').ToArray();
            arrayInt = arrayInt.Where(a => a < numeroDiferente).ToArray();

            Console.WriteLine("Array Original: [{0}]", string.Join(", ", arrayInt));
            return arrayInt;
        }

        //*******************************************
        //MÉTODO PARA FAZER A ROTAÇÃO CIRCULAR
        //DOS NÚMEROS DO TOKEN VALIDADO PELO
        //PRIMEIRO ALGORITMO DE VALIDAÇÃO
        //O NÚMERO DE ROTAÇÕES É O CVV INFORMADO
        //*******************************************
        public int[] CircularRotation(int[] arrayInt)
        {
            var lastElement = arrayInt[arrayInt.Length - 1];
            var arrayResult = arrayInt.Take(arrayInt.Length - 1).ToArray(); //obtém array sem o último elemento
            var listElements = arrayResult.ToList<int>();
            listElements.Insert(0, lastElement);

            arrayResult = listElements.ToArray();

            return arrayResult;
        }
    }
}