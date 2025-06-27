using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Common.ResponseHandler
{
    public static class ResponseStatusMessages
    {
        private static IDictionary<string, string> StatusMessagesCollection = new Dictionary<string, string>()
        {
            { "Ok", "¡La solicitud ha tenido éxito!" },
            { "Created", "¡El registro se ha creado exitosamente!" },
            { "Updated", "¡El registro se ha modificado exitosamente!" },
            { "Deleted", "¡El registro se ha eliminado exitosamente!" },
            { "Accepted", "La tarea se ha puesto en ejecución..." },
            { "NoContent", "" },
            { "BadRequest", "¡Ha ocurrido un error, asegurese de completar los campos o contáctese con un administrador!" },
            { "InvalidCredentials", "¡Las credenciales ingresadas no son válidas!" },
            { "Unauthorized", "¡Debe iniciar sesión para realizar esta acción!" },
            { "Forbidden", "¡Usted no cuenta con los permisos necesario para realizar esta acción, contáctese con un administrador!" },
            { "NotFound", "¡El recurso que usted ha solicitado no ha sido encontrado!" },
            { "MethodNotAllowed", "¡El recurso que usted ha solicitado no se encuentra disponible!" },
            { "InternalServerError", "¡Ha ocurrido un error, contáctese con un administrador para notificar el incidente!" },
            { "NotImplemented", "¡No se reconoce el recurso que usted ha solicitado!" },
            { "ServiceUnavalible", "¡El recurso que usted ha solicitado no se encuentra disponible en estos momentos!" }
        };

        protected enum StatusCases //Definición por contexto de las cabeceras de respuesta de las peticiones
        {
            //2xx
            Ok = 200,                 //Exitoso
            Created = 201,            //Recurso Creado
            Accepted = 202,           //Petición aceptada pero aún no procesada
            NoContent = 204,          //Petición realizada con éxito pero no hay cuerpo de respuesta

            //4xx
            BadRequest = 400,         //Error genérico. No se puede completar la petición por error del cliente. Regularmente se envia en el cuerpo de la respuesta la instrucción acerca de que se hizo mal
            Unauthorized = 401,       //Esto indica que el cliente requiere de una previa autorización para acceder al recurso
            Forbidden = 403,          //No hay permisos para utilizar el recurso que solicito aunque este logueado
            NotFound = 404,           //Recurso no encontrado
            MethodNotAllowed = 405,   //El método-http utilizado no está disponible para el recurso solicitado

            //5xx
            InternalServerError = 500,//Este es un error genérico. Regularmente se envia en el cuerpo de la respuesta la instrucción acerca de que salió mal.
            NotImplemented = 501,     //Se da cuando el servidor no reconoce ni soporta el método-http usado en la petición del cliente.
            ServiceUnavalible = 503   //El servicio no se encuentra disponible en estos momentos, típicamete esto es algo temporal
        }

        public static string GetStatusMessageResponse(int statusCode)
        {
            // Convertir el código de estado al nombre del enum, si existe
            if (Enum.IsDefined(typeof(StatusCases), statusCode))
            {
                var statusName = ((StatusCases)statusCode).ToString();

                // Verificar si el diccionario contiene el mensaje correspondiente
                if (StatusMessagesCollection.TryGetValue(statusName, out string? mensaje))
                {
                    return mensaje; // Retornar el mensaje del diccionario
                }
            }

            // Retornar un mensaje por defecto si el estado no existe en el diccionario
            return "Código de estado no reconocido.";
        }

    }
}
