using Foundation.BlazorExtensions.CustomBlazorRouter;
using SitecoreBlazorHosted.Shared;
using SitecoreBlazorHosted.Shared.Models;
using SitecoreBlazorHosted.Shared.Models.Sitecore;
using System.Collections.Generic;

namespace Foundation.BlazorExtensions.Services
{
    public class SitecoreItemService
    {
        private const string PageComponent = "SitecoreBlazorHosted.Client.Shared.MasterBlaster, SitecoreBlazorHosted.Client";
        public ScItem GetSitecoreItemRootMock(string language = "en")
        {

            return language == "sv"
                      ? new ScItem()
                      {
                          Name = "Home",
                          Url = "/sv",
                          Id = "dac24edd-44fb-42ef-9ecd-1e8daf706386",
                          Language = "sv",
                          Fields = new List<IBlazorSitecoreField>()
                  {
                        new BlazorSitecoreField<string>
                        {
                            FieldName = "NavigationTitle",
                            Value = "Hem",
                            Editable = "Hem",
                            Type =  FieldTypes.PlainTextField
                        }
                  },
                          Children = new List<ISitecoreItem>()
                      {
                                new ScItem()
                                {
                                    Name = "Habitat stuff",
                                    Url = "/sv/habitat-stuff",
                                    Id = "762684e7-480b-41d6-82de-8549babe95e3",
                                    Language = "sv",
                                    Parent = new ScItem()
                                    {
                                        Name = "Home",
                                        Id = "dac24edd-44fb-42ef-9ecd-1e8daf706386"
                                    },
                                    Fields = new List<IBlazorSitecoreField>()
                                    {
                                            new BlazorSitecoreField<string>
                                            {
                                                FieldName = "NavigationTitle",
                                                Value = "Habitat grejer",
                                                Editable = "Habitat grejer",
                                                Type =  FieldTypes.PlainTextField
                                            }
                                    },
                                    Children = new List<ISitecoreItem>()
                                    {
                                        new ScItem()
                                        {
                                            Name = "Carousels",
                                            Url = "/sv/habitat-stuff/carousels",
                                            Id = "8a80477e-7cb4-4cee-a035-b48ac118abe8",
                                            Language = "sv",
                                            Fields = new List<IBlazorSitecoreField>()
                                            {
                                                    new BlazorSitecoreField<string>
                                                    {
                                                        FieldName = "NavigationTitle",
                                                        Value = "Karuseller",
                                                        Editable = "Karuseller",
                                                        Type =  FieldTypes.PlainTextField
                                                    }
                                            },
                                            Parent = new ScItem()
                                            {
                                                Name = "Habitat stuff",
                                                Id = "762684e7-480b-41d6-82de-8549babe95e3"
                                            }

                                        },
                                        new ScItem()
                                        {
                                            Name = "Teasers",
                                            Url = "/sv/habitat-stuff/teasers",
                                            Id="f30c4470-07d4-4652-9cb6-bf8ac43d1694",
                                            Language = "sv",
                                            Fields = new List<IBlazorSitecoreField>()
                                            {
                                                    new BlazorSitecoreField<string>
                                                    {
                                                        FieldName = "NavigationTitle",
                                                        Value = "Puffar",
                                                        Editable = "Puffar",
                                                        Type =  FieldTypes.PlainTextField
                                                    }
                                            },
                                            Parent = new ScItem()
                                            {
                                                Name = "Habitat stuff",
                                                Id = "762684e7-480b-41d6-82de-8549babe95e3"
                                            }

                                        }
                                    }

                                },
                                new ScItem()
                                {
                                    Name = "Weather",
                                    Url = "/sv/weather",
                                    Id = "a2c07e48-9c6a-466e-b55d-baac62e4dfc7",
                                    Language = "sv",
                                    Fields = new List<IBlazorSitecoreField>()
                                    {
                                            new BlazorSitecoreField<string>
                                            {
                                                FieldName = "NavigationTitle",
                                                Value = "Väder",
                                                Editable = "Väder",
                                                Type =  FieldTypes.PlainTextField
                                            }
                                    },
                                    Parent = new ScItem()
                                    {
                                        Name = "Home",
                                        Id = "dac24edd-44fb-42ef-9ecd-1e8daf706386"
                                    }

                                }


                      }
                      }
                      : new ScItem()
                      {
                          Name = "Home",
                          Url = "/en",
                          Id = "dac24edd-44fb-42ef-9ecd-1e8daf706386",
                          Language = "en",
                          Fields = new List<IBlazorSitecoreField>()
                      {
                            new BlazorSitecoreField<string>
                            {
                                FieldName = "NavigationTitle",
                                Value = "Home",
                                Editable = "Home",
                                Type =  FieldTypes.PlainTextField
                            }
                      },
                          Children = new List<ISitecoreItem>()
                      {
                              new ScItem()
                                {
                                    Name = "Habitat stuff",
                                    Url = "/en/habitat-stuff",
                                    Id = "762684e7-480b-41d6-82de-8549babe95e3",
                                    Language = "en",
                                    Parent = new ScItem()
                                    {
                                        Name = "Home",
                                        Id = "dac24edd-44fb-42ef-9ecd-1e8daf706386"
                                    },
                                    Fields = new List<IBlazorSitecoreField>()
                                    {
                                            new BlazorSitecoreField<string>
                                            {
                                                FieldName = "NavigationTitle",
                                                Value = "Habitat stuff",
                                                Editable = "Habitat stuff",
                                                Type =  FieldTypes.PlainTextField
                                            }
                                    },
                                    Children = new List<ISitecoreItem>()
                                    {
                                        new ScItem()
                                        {
                                            Name = "Carousels",
                                            Url = "/en/habitat-stuff/carousels",
                                            Id = "8a80477e-7cb4-4cee-a035-b48ac118abe8",
                                            Language = "en",
                                            Fields = new List<IBlazorSitecoreField>()
                                            {
                                                    new BlazorSitecoreField<string>
                                                    {
                                                        FieldName = "NavigationTitle",
                                                        Value = "Carousels",
                                                        Editable = "Carousels",
                                                        Type =  FieldTypes.PlainTextField
                                                    }
                                            },
                                            Parent = new ScItem()
                                            {
                                                Name = "Habitat stuff",
                                                Id = "762684e7-480b-41d6-82de-8549babe95e3"
                                            }

                                        },
                                        new ScItem()
                                        {
                                            Name = "Teasers",
                                            Url = "/en/habitat-stuff/teasers",
                                            Id="f30c4470-07d4-4652-9cb6-bf8ac43d1694",
                                            Language = "en",
                                            Fields = new List<IBlazorSitecoreField>()
                                            {
                                                    new BlazorSitecoreField<string>
                                                    {
                                                        FieldName = "NavigationTitle",
                                                        Value = "Teasers",
                                                        Editable = "Teasers",
                                                        Type =  FieldTypes.PlainTextField
                                                    }
                                            },
                                            Parent = new ScItem()
                                            {
                                                Name = "Habitat stuff",
                                                Id = "762684e7-480b-41d6-82de-8549babe95e3"
                                            }

                                        }
                                    }

                                },
                                new ScItem()
                                {
                                    Name = "Weather",
                                    Url = "/en/weather",
                                    Id = "a2c07e48-9c6a-466e-b55d-baac62e4dfc7",
                                    Language = "en",
                                    Fields = new List<IBlazorSitecoreField>()
                                    {
                                            new BlazorSitecoreField<string>
                                            {
                                                FieldName = "NavigationTitle",
                                                Value = "Weather",
                                                Editable = "Weather",
                                                Type =  FieldTypes.PlainTextField
                                            }
                                    },
                                    Parent = new ScItem()
                                    {
                                        Name = "Home",
                                        Id = "dac24edd-44fb-42ef-9ecd-1e8daf706386"
                                    }

                                }


                      }
                      };


        }

        public RouterDataRoot ConfigRoutes(string language = "en")
        {

            ISitecoreItem rootItem = GetSitecoreItemRootMock();

            RouterDataRoot routesData = new RouterDataRoot()
            {
                //Home
                Routes = new List<RouterData>()
                {
                  new RouterData()
                  {
                    Path = "/{Language}",
                    Page = PageComponent
                  }
                }
            };

            //Rest of menu items
            foreach (ISitecoreItem item in rootItem.Children)
            {
                routesData.Routes.Add(new RouterData()
                {
                    Path = "/{Language}" + item.Url.Substring(language.Length + 1),
                    Page = PageComponent,
                    Children = item.HasChildren ? GetChildren(item) : null
                });
            }

            //Fallback error handling
            routesData.Routes.Add(new RouterData()
            {
                Path = "/{Language}/{PageUrl}",
                Page = PageComponent,
                Children = new List<RouterData>()
                {
                    new RouterData()
                    {
                        Path = "/{ChildLevel1}",
                        Page = PageComponent,
                        Children = new List<RouterData>()
                        {
                            new RouterData()
                            {
                                Path = "/{ChildLevel2}",
                                Page = PageComponent
                            }
                        }
                    }
                }
            });

           

            return routesData;



            List<RouterData> GetChildren(ISitecoreItem sitecoreItem)
            {
                List<RouterData> children = new List<RouterData>();

                if (!sitecoreItem.HasChildren)
                {
                    return null;
                }

                foreach (ISitecoreItem child in sitecoreItem.Children)
                {
                    children.Add(new RouterData()
                    {
                        Path = child.Url.Substring(child.Url.LastIndexOf('/')),
                        Page = PageComponent,
                        Children = child.HasChildren ? GetChildren(child) : null
                    });

                }

                return children;
            }
        }
    }
}
