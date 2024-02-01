using EticaretApp.Application.Abstractions.Services.Configurations;
using EticaretApp.Application.CustomAttribute;
using EticaretApp.Application.DTO_s.Configuration;
using EticaretApp.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApp.Infsrastructure.Services2.Configuration
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndPoint(Type type)
        {
            
                Assembly assembly = Assembly.GetAssembly(type);
                var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

                List<Menu> menus = new();
                if (controllers != null)
                    foreach (var controller in controllers)
                    {
                        var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                        if (actions != null)
                            foreach (var action in actions)
                            {
                                var attributes = action.GetCustomAttributes(true);
                                if (attributes != null)
                                {
                                    Menu menu = null;

                                    var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                    if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                                    {
                                        menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                        menus.Add(menu);
                                    }
                                    else
                                        menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);

                                    Application.DTO_s.Configuration.Action _action = new()
                                    {
                                        ActionTypes = authorizeDefinitionAttribute.ActionType,
                                        Definition = authorizeDefinitionAttribute.Definition
                                    };

                                    var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                    if (httpAttribute != null)
                                        _action.HttpType = httpAttribute.HttpMethods.First();
                                    else
                                        _action.HttpType = HttpMethods.Get;

                                    menu.Actions.Add(_action);
                                }
                            }
                    }


                return menus;
            }
        }
    }