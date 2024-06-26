using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaqueController : ControllerBase
    {

        [HttpPost("withdraw/{value}")]
        //IActionResult to return a JSON.
        public IActionResult Withdraw(int value)
        {
            try
            {

                int?[] notas = { 100, 50, 20, 10, 5, 2 };
                //Create a condition when value is negative ou odd
                if(value <= 0 || value % 2 != 0)
                {
                    throw new ArgumentException("Valor invalido, o valor não pode ser negativo ou impar");
                }

                //Create a Dictionary to store the amount of each note denomination withdraw
                Dictionary<int, int?> notasCount = new Dictionary<int, int?>();
                //Use a foreach loop to initialize the dictionary with all denominations, all initially with a value 0.
                foreach (var nota in notas)
                {
                    
                    notasCount[nota ?? 0] = 0;
                }
                for (int i = 0; i < notas.Length; i++)
                {
                    if(notas[i] == null || notas[i] == 0)
                        continue;

                    //Use if for checks if the value is less than 10, to number like 388, that ending less than 10.
                    if (value < 10 && value % notas[i] != 0)
                        continue;
                    
                    //Create a variable to know the quotient.
                    int quociente = value / notas[i] ?? 0;
                    //Store the value in the variable.
                    notasCount[notas[i] ?? 0] = quociente;
                    //Calculation to get the rest.
                    value = value - quociente * notas[i] ?? 0;
                }

                return Ok(notasCount);
            }
            catch (ArgumentException ae)
            {

                return Ok(ae.Message);
            }

        }
    }
}