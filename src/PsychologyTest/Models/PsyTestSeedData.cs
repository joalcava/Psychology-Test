using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PsychologyTest.Controllers;
using Microsoft.EntityFrameworkCore;

namespace PsychologyTest.Models
{
    public class PsyTestSeedData
    {
        private PsyTestContext _context;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<PsyTestUser> _userManager;

        public PsyTestSeedData(PsyTestContext context,
                               UserManager<PsyTestUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }


        public async Task SeedDataTask()
        {
            // Rol de ROOT
            if (!await _roleManager.RoleExistsAsync("Root"))
            {
                var roleRoot = new IdentityRole();
                roleRoot.Name = "Root";
                await _roleManager.CreateAsync(roleRoot);
            }

            // Rol de Administrador
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var roleAdmin = new IdentityRole();
                roleAdmin.Name = "Admin";
                await _roleManager.CreateAsync(roleAdmin);
            }

            // Rol de Psicologo
            if (!await _roleManager.RoleExistsAsync("Psy"))
            {
                var rolePsy = new IdentityRole();
                rolePsy.Name = "Psy";
                await _roleManager.CreateAsync(rolePsy);
            }

            // Rol de Estudiante
            if (!await _roleManager.RoleExistsAsync("Stud"))
            {
                var roleStud = new IdentityRole();
                roleStud.Name = "Stud";
                await _roleManager.CreateAsync(roleStud);
            }

            // Creacion de usuario Root
            if (await _userManager.FindByEmailAsync("su@root.com") == null)
            {
                var userRoot = new PsyTestUser()
                {
                    UserName = "su@root.com",
                    Email = "su@root.com",
                    EmailConfirmed = true,
                    Activo = true
                };
                var resultRoot = await _userManager.CreateAsync(userRoot, "Root123456");
                if (resultRoot.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userRoot, "Root");
                    await _userManager.AddClaimAsync(userRoot, new Claim("emailconfirmation", "1"));
                }
            }

            // Creacion de usuario administrador
            if (await _userManager.FindByEmailAsync("sys@admin.com") == null)
            {
                var userAdmin = new PsyTestUser()
                {
                    UserName = "sys@admin.com",
                    Email = "sys@admin.com",
                    EmailConfirmed = true,
                    Activo = true
                };
                var resultAdmin = await _userManager.CreateAsync(userAdmin, "Admin123456");
                if (resultAdmin.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userAdmin, "Admin");
                    await _userManager.AddClaimAsync(userAdmin, new Claim("emailconfirmation", "1"));
                }
            }

            // --------------------------------------------------------------------------------- //
            //               Descomentar unicamente luego de un database drop                    //
            // --------------------------------------------------------------------------------- //
            
            /*
            // Creacion de instititucion de pruebas
            var insti = new Institucion
            {
                Nit = 123456,
                Nombre = "Institucion de pruebas",
                Direccion = "enseguida del vecino",
                SitioWeb = "psychologytest.azurewebsites.net",
                Telefono = "0000000",
                Grupos = new List<Grupo>()
                    {
                        new Grupo
                        {
                            Grado = "1",
                            Jornada = "Completa",
                            Nombre = "Grupo de pruebas"
                        }
                    }
            };
            _context.Instituciones.Add(insti);
            _context.SaveChanges();


            // Creacion de estudiante de pruebas
            var userStud = new PsyTestUser
            {
                UserName = "stud@test.com",
                Email = "stud@test.com",
                EmailConfirmed = true,
                Activo = true,
                Nombres = "Estudiante de prueba"
            };
            var resultStud = await _userManager.CreateAsync(userStud, "Stud123456");
            if (resultStud.Succeeded)
            {
                await _userManager.AddToRoleAsync(userStud, "Stud");
                await _userManager.AddClaimAsync(userStud, new Claim("emailconfirmation", "1"));
                var stud = new Estudiante
                {
                    Usuario = userStud,
                    Institucion = _context.Instituciones.First(inst => inst.Nit == 123456),
                    Grupo = _context.Instituciones.Include(i => i.Grupos).First(inst => inst.Nit == 123456).Grupos.First()
                };
                _context.Estudiantes.Add(stud);
            }

            //TODO: Semilla para un psicologo de pruebas.


            // Creacion de Test de Kolb
            var Kolb = new PruebaPsicologica
            {
                Nombre = "Test de Kolb",
                Descripcion = "En este test se te pide que completes 12 frases. Cada frase puede" +
                              " terminarse de cuatro formas distintas. Ordena las cuatro opciones de cada" +
                              "frase segun pienses que se ajustan  a tu manera de aprender algo nuevo, talvez" +
                              " en tu trabajo. Trata de pensar en situaicones recientes en las que te enfrentaste con algo nuevo." +
                              "Numera con un 4 la terminacion que mejor se ajuste a tu forma de aprender y con un 1 la que peor " +
                              "se ajuste. Asegurate de asignar un numero a todas las terminaciones de cada una de las 12 frases.",
                FechaCreado = DateTime.Now,
                Activo = true,
                Preguntas = new List<Pregunta>
                {
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Cuando aprendo",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Me gusta vivir sensaciones", Valor = 1},
                            new Opcion {Texto = "Me gusta pensar sobre ideas", Valor = 2},
                            new Opcion {Texto = "Me gusta estar haciendo cosas", Valor = 3},
                            new Opcion {Texto = "Me gusta observar y escuchar", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 1
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Aprendo meojor cuando...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Escucho y observo cuidadosamente", Valor = 0},
                            new Opcion {Texto = "Confio en el pensamiento logico", Valor = 2},
                            new Opcion {Texto = "Confio en mi intuicion y sentimientos", Valor = 3},
                            new Opcion {Texto = "Trabajo duro para lograr hacer las cosas", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 2
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Cuando estoy aprendiendo...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Tiendo a usar el razonamiento", Valor = 0},
                            new Opcion {Texto = "Soy responsable con lo que hago", Valor = 2},
                            new Opcion {Texto = "Soy callado y reservado", Valor = 3},
                            new Opcion {Texto = "Tengo fuertes sensaciones y reacciones", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 3
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Yo aprendo...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Sintiendo", Valor = 0},
                            new Opcion {Texto = "Haciendo", Valor = 2},
                            new Opcion {Texto = "Observando", Valor = 3},
                            new Opcion {Texto = "Pensando", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 4
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Cuando aprendo...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Estoy abierto a nuevas experiencias", Valor = 0},
                            new Opcion {Texto = "Observo todos los aspectos del asunto", Valor = 2},
                            new Opcion
                            {
                                Texto = "Me gusta analizar las cosas, descomponerlas en sus partes",
                                Valor = 3
                            },
                            new Opcion {Texto = "Me gusta probar e intentar hacer las cosas", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 5
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Cuando estoy aprendiendo...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Soy unapersona observadora", Valor = 0},
                            new Opcion {Texto = "Soy una persona activa", Valor = 2},
                            new Opcion {Texto = "Soy una persona intuitiva", Valor = 3},
                            new Opcion {Texto = "Soy una persona logica", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 6
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Yo aprendo mejor de...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "La observacion", Valor = 0},
                            new Opcion {Texto = "La relacion con otras personas", Valor = 2},
                            new Opcion {Texto = "Las teorias racionales", Valor = 3},
                            new Opcion {Texto = "La oportunidad de probar y practicar", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 7
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Cuando aprendo...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Me gusta ver los resultados de mi trabajo", Valor = 0},
                            new Opcion {Texto = "Me gustan las ideas y teorias", Valor = 2},
                            new Opcion {Texto = "Me tomo mi tiempo antes de actuar", Valor = 3},
                            new Opcion {Texto = "Me siento personalmente involucrado en las cosas", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 8
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Aprendo mejor cuando...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Confio en mis observaciones", Valor = 0},
                            new Opcion {Texto = "Confio en mis sentimientos", Valor = 2},
                            new Opcion {Texto = "Puedo probar por mi cuenta", Valor = 3},
                            new Opcion {Texto = "Confio en mis ideas", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 9
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Cuando estoy aprendiendo...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Soy una persona reservada", Valor = 0},
                            new Opcion {Texto = "Soy una persona receptiva", Valor = 2},
                            new Opcion {Texto = "Soy una persona responsable", Valor = 3},
                            new Opcion {Texto = "soy una persona racional", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 10
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Cuando aprendo...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Me involucro", Valor = 0},
                            new Opcion {Texto = "Me gusta observar", Valor = 2},
                            new Opcion {Texto = "Evaluo las cosas", Valor = 3},
                            new Opcion {Texto = "Me gusta ser activo", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 11
                    },
                    new PreguntaDeOpcionMultiple
                    {
                        Descripcion = "Aprendo mejor cuando...",
                        Opciones = new List<Opcion>
                        {
                            new Opcion {Texto = "Analizo ideas", Valor = 0},
                            new Opcion {Texto = "Soy receptivo", Valor = 2},
                            new Opcion {Texto = "Soy cuidadoso", Valor = 3},
                            new Opcion {Texto = "Soy practico", Valor = 4}
                        },
                        MultiplesRespuestas = true,
                        RespuestaConValorDeVerdad = true,
                        Posicion = 12
                    },
                }
            };
            _context.PruebasPsicologicas.Add(Kolb);
            await _context.SaveChangesAsync();


            // Creacion de una respuesta para el test de kolb
            var respuesta = new PruebaPsicologicaAsignada
            {
                PruebaAsignada = _context.PruebasPsicologicas.First(p => p.Nombre == "Test de Kolb"),
                Encuestado = _context.Estudiantes.First(e => e.Usuario.UserName == "stud@test.com"),
                FechaAsignacion = DateTime.Now,
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now,
                Iniciado = true,
                Completado = true,
                Respuestas = new List<Respuesta>()
                 {
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 1),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 1 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 3 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 3 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 2),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 3 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 1 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 4 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 3),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 3 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 3 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 4),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 1 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 2 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 5),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 4 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 6),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 2 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 2 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 4 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 7),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 1 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 3 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 4 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 8),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 1 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 1 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 2 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 9),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 3 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 4 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 10),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 3 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 3 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 4 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 11),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 2 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 4 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 2 }
                         }
                     },
                     new MultiplesRespuestasConValor
                     {
                         Pregunta = _context.PruebasPsicologicas.
                                        Include(a => a.Preguntas).
                                        First(p => p.Nombre == "Test de Kolb").
                                        Preguntas.Find(b => b.Posicion == 12),
                         FechaRespondida = DateTime.Now,
                         Respuestas = new List<OpcionConValorDeVerdad>()
                         {
                             new OpcionConValorDeVerdad { Opcion = 1, Valor = 3 },
                             new OpcionConValorDeVerdad { Opcion = 2, Valor = 1 },
                             new OpcionConValorDeVerdad { Opcion = 3, Valor = 3 },
                             new OpcionConValorDeVerdad { Opcion = 4, Valor = 4 }
                         }
                     }
                 }
            };
            _context.PruebasPsicologicaAsignadas.Add(respuesta);
            */
            // --------------------------------------------------------------------------------- //

            await _context.SaveChangesAsync();
        }
    }
}
