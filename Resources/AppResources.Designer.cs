﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace voltaire.Resources {
    using System;
    using System.Reflection;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class AppResources {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("voltaire.Resources.AppResources", typeof(AppResources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string Username {
            get {
                return ResourceManager.GetString("Username", resourceCulture);
            }
        }
        
        internal static string Password {
            get {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }
        
        internal static string Login {
            get {
                return ResourceManager.GetString("Login", resourceCulture);
            }
        }
        
        internal static string Dashboard {
            get {
                return ResourceManager.GetString("Dashboard", resourceCulture);
            }
        }
        
        internal static string Goals {
            get {
                return ResourceManager.GetString("Goals", resourceCulture);
            }
        }
        
        internal static string TopSalesman {
            get {
                return ResourceManager.GetString("TopSalesman", resourceCulture);
            }
        }
        
        internal static string Sales {
            get {
                return ResourceManager.GetString("Sales", resourceCulture);
            }
        }
        
        internal static string CheckIn {
            get {
                return ResourceManager.GetString("CheckIn", resourceCulture);
            }
        }
        
        internal static string Contacts {
            get {
                return ResourceManager.GetString("Contacts", resourceCulture);
            }
        }
        
        internal static string Filters {
            get {
                return ResourceManager.GetString("Filters", resourceCulture);
            }
        }
    }
}
