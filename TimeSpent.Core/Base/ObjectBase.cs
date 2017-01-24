using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Linq.Expressions;
using TimeSpent.Core.Utils;
using System.Runtime.Serialization;
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using TimeSpentCore.Extensions;
using TimeSpent.Core.Contracts;
using System.Collections;
using System.ComponentModel.Composition.Hosting;

namespace TimeSpent.Core
{
    public class ObjectBase :NotificationObject, IExtensibleDataObject, IDataErrorInfo, IDirtyCapable
    {

        public ObjectBase()
        {
            _validator = GetValidator();
            Validate();
        }

        public static CompositionContainer Container { get; set; }
        protected IValidator _validator = null;
        protected IEnumerable<ValidationFailure> _validationErrors = null;
        private bool _isDirty;

        #region IExtensibleDataObject members

        public ExtensionDataObject ExtensionData { get; set; }

        #endregion

        #region IDirtyCapable implementation

       

        [NotNavigable]
        public virtual bool IsDirty
        {
            get { return _isDirty; }

            set
            {
                _isDirty = value;
                OnPropertyChanged("IsDirty", false);
            }
        }


        public bool IsAnythingDirty()
        {
            bool isDirty = false;

            WalkObjectGraph(
                o =>
                {
                    if (o.IsDirty)
                    {
                        isDirty = true;
                        return true;
                    }
                    else
                        return false;
                }, col => { });

            return isDirty;

        }

        public List<IDirtyCapable> GetDirtyObjects()
        {
            List<IDirtyCapable> dirtyObjects = new List<IDirtyCapable>();

            WalkObjectGraph(
                o =>
                {
                    if (o.IsDirty)
                        dirtyObjects.Add(o);
                    return false;

                }, coll => { });

            return dirtyObjects;
        }

        public void CleanAll()
        {
            WalkObjectGraph(
                o =>
                {
                    if (o.IsDirty)
                        o.IsDirty = false;

                    return false;
                }, coll => { });
        }

        #endregion

        #region protected methods
        protected void WalkObjectGraph(Func<ObjectBase, bool> snippetForObject,
                                  Action<IList> snippetForCollection,
                                  params string[] exemptProperties)
        {
            List<ObjectBase> visited = new List<ObjectBase>();
            Action<ObjectBase> walk = null;

            List<string> exemptions = new List<string>();
            if (exemptProperties != null)
                exemptions = exemptProperties.ToList();

            walk = (o) =>
            {
                if (o != null && !visited.Contains(o))
                {
                    visited.Add(o);

                    bool exitWalk = snippetForObject.Invoke(o);

                    if (!exitWalk)
                    {
                        PropertyInfo[] properties = o.GetBrowsableProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            if (!exemptions.Contains(property.Name))
                            {
                                if (property.PropertyType.IsSubclassOf(typeof(ObjectBase)))
                                {
                                    ObjectBase obj = (ObjectBase)(property.GetValue(o, null));
                                    walk(obj);
                                }
                                else
                                {
                                    IList coll = property.GetValue(o, null) as IList;
                                    if (coll != null)
                                    {
                                        snippetForCollection.Invoke(coll);

                                        foreach (object item in coll)
                                        {
                                            if (item is ObjectBase)
                                                walk((ObjectBase)item);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            walk(this);
        }

        #endregion



        #region property change notofication

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression, bool makeDirty)
        {
            string propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName, true);
        }

        protected virtual void OnPropertyChanged(string propertyName, bool makeDirty)
        {
            //_propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
            base.OnPropertyChanged(propertyName);
            if (makeDirty)
                _isDirty = true;

            Validate();
        }

      
      
        #endregion

      
      

     
        

      
       

        #region Validation

        protected virtual IValidator GetValidator()
        {
            return null;
        }

        [NotNavigable]
        public IEnumerable<ValidationFailure> ValidationErros
        {
            get
            {
                return _validationErrors;
            }

            set { }
        }


        public void Validate()
        {
            if(_validator != null)
            {
                ValidationResult result = _validator.Validate(this);
                _validationErrors = result.Errors;
            }
        }

        public virtual bool IsValid
        {
            get
            {
                if (_validationErrors != null && _validationErrors.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion


        #region IDataErrorInfo members
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                StringBuilder errors = new StringBuilder();

                if(_validationErrors != null && _validationErrors.Count() > 0)
                {
                    foreach(ValidationFailure e in _validationErrors)
                    {
                        if(e.PropertyName == columnName)
                        {
                            errors.AppendLine(e.ErrorMessage);
                        }
                    }
                }
                return errors.ToString();
            }
        }

        #endregion

    
          }
}
