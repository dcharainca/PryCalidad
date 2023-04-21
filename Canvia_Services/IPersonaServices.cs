using Canvia_DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Canvia_Services
{
    public interface IPersonaServices
    {

        bool Insertar(Persona_DTO BE);
        bool Eliminar(int personaId);
        bool AnulacionLogica(int personaId);
        bool Actualizar(int personaId,Persona_DTO BE);
        List<Persona_DTO> Listar();
        List<Persona_DTO> ListarById(int personaId);


    }
}
