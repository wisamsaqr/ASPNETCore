﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCIntro.ZMyResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Lang {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Lang() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MVCIntro.ZMyResources.Lang", typeof(Lang).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Address.
        /// </summary>
        public static string Address {
            get {
                return ResourceManager.GetString("Address", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to E-mail.
        /// </summary>
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please Enter valid email address.
        /// </summary>
        public static string EmailValidationMsg {
            get {
                return ResourceManager.GetString("EmailValidationMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Female.
        /// </summary>
        public static string Female {
            get {
                return ResourceManager.GetString("Female", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Full Name.
        /// </summary>
        public static string FullName {
            get {
                return ResourceManager.GetString("FullName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gender.
        /// </summary>
        public static string Gender {
            get {
                return ResourceManager.GetString("Gender", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Male.
        /// </summary>
        public static string Male {
            get {
                return ResourceManager.GetString("Male", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mobile.
        /// </summary>
        public static string Mobile {
            get {
                return ResourceManager.GetString("Mobile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Register.
        /// </summary>
        public static string Register {
            get {
                return ResourceManager.GetString("Register", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please Enter this field.
        /// </summary>
        public static string RequiredValidationMsg {
            get {
                return ResourceManager.GetString("RequiredValidationMsg", resourceCulture);
            }
        }
    }
}