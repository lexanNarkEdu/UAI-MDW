using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BE
{
    /// <summary>
    /// Interfaz que va a implementar toda BL y DAL de la entidad que se desee verificar su integridad
    /// </summary>
    public interface IVerificable
    {
         List<DataRow> ObtenerRegistros();
    }


}
