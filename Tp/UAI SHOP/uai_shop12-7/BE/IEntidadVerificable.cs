using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Servicios
{/// <summary>
/// Interfaz que implementara la BE que se desee verificar su integridad
/// </summary>
    public interface IEntidadVerificable
    {/// <summary>
    /// Este metodo debe devolver la clave primaria de la entidad que la implemente tal y como esta almacenado en la base de datos
    /// </summary>
    /// <returns></returns>
        int GetDbKey();


    }
}
