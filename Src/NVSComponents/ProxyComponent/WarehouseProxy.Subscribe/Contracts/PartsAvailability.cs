﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.17929.
// 
namespace Volvo.POS.Proxy.Warehouse.Subscribe.Contracts
{
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class PartsAvailability {
        
        private PartsAvailabilityPart[] partsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Part", IsNullable=false)]
        public PartsAvailabilityPart[] Parts {
            get {
                return this.partsField;
            }
            set {
                this.partsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class PartsAvailabilityPart {
        
        private long numberField;
        
        private bool numberFieldSpecified;
        
        private decimal newAvailabilityField;
        
        private bool newAvailabilityFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public long Number {
            get {
                return this.numberField;
            }
            set {
                this.numberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NumberSpecified {
            get {
                return this.numberFieldSpecified;
            }
            set {
                this.numberFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal NewAvailability {
            get {
                return this.newAvailabilityField;
            }
            set {
                this.newAvailabilityField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NewAvailabilitySpecified {
            get {
                return this.newAvailabilityFieldSpecified;
            }
            set {
                this.newAvailabilityFieldSpecified = value;
            }
        }
    }
}