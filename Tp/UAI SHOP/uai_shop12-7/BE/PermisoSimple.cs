using System.Collections.Generic;

namespace BE
{
    public class PermisoSimple : Perfil
    {
        public override List<Perfil> GetPermisos()
        {
            return new List<Perfil> { this };
        }
    }
}
