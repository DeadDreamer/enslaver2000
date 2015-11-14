using EnslaverFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnslaverFrontEnd.Logic
{ /// <summary>
    /// Фабрика и контроллер форм в одном лице
    /// </summary>
    public class FormFactory
    {
        public FormFactory()
        {
        }

        /// <summary>
        /// Регистрирует форму для создания через фабрику
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="typeOfForm"></param>
        public virtual void RegisterInFactoryFormByType(long typeID, Type typeOfForm)
        {
            DicTypeIDToForm[typeID] = typeOfForm;
        }

        //Справочник для создания форм через регистрацию
        protected Dictionary<long, Type> DicTypeIDToForm = new Dictionary<long, Type>();

        /// <summary>
        /// Справочник текщих активных окон
        /// </summary>
        public Dictionary<Guid, BaseForm> DicOfForms = new Dictionary<Guid, BaseForm>();

        private BaseForm _startForm = null;

        public virtual BaseForm StartForm
        {
            get { return _startForm; }
        }

        /// <summary>
        /// контекст приложения, содержит в себе главную форму
        /// </summary>
        private ApplicationContext _MainApplicationContext;
        public virtual ApplicationContext MainApplicationContext
        {
            get
            {
                if (_MainApplicationContext == null)
                {
                    if (StartForm.Guid == Guid.Empty)
                    {
                        StartForm.Guid = Guid.NewGuid();
                    }
                    DicOfForms[StartForm.Guid] = StartForm;
                    _MainApplicationContext = new ApplicationContext(StartForm);
                }
                return _MainApplicationContext;
            }
            set
            {
                _MainApplicationContext = value;
            }
        }

        /// <summary>
        /// Фабрика для создания форм
        /// </summary>
        /// <param name="formType"></param>
        /// <param name="formMessage">Сообщение для инициализации формы</param>
        /// <returns></returns>
        public virtual BaseForm CreateForm(long formTypeID, FormMessage formMessage)
        {
            if (DicTypeIDToForm.ContainsKey(formTypeID))
            {
                return (BaseForm)Activator.CreateInstance(DicTypeIDToForm[formTypeID], (this));
            }
            throw new Exception("Form not found");
        }


        /// <summary>
        /// Переход на форму с удалением той, что создала запрос
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="formTypeID"></param>
        /// <returns></returns>
        public virtual BaseForm MoveToForm(BaseForm sender, long formTypeID)
        {
            return MoveToForm(sender, formTypeID, null);
        }

        /// <summary>
        /// Переход на форму с удалением той, что создала запрос
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="formTypeID"></param>
        /// <param name="formMessage"></param>
        /// <returns></returns>
        public virtual BaseForm MoveToForm(BaseForm sender, long formTypeID, FormMessage formMessage)
        {
            BaseForm CurrentForm = null;
            CurrentForm = CreateForm(formTypeID, formMessage);
            MainApplicationContext.MainForm = CurrentForm;
            if (sender != null)
                CurrentForm.SenderGuid = sender.Guid;
            DicOfForms[CurrentForm.Guid] = CurrentForm;
            if (sender != null)
            {
                DicOfForms.Remove(sender.Guid);
                sender.Close();
            }
            CurrentForm.Show();
            return CurrentForm;
        }



        /// <summary>
        /// Создание невидимой формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="formTypeID"></param>
        /// <param name="formMessage"></param>
        /// <returns></returns>
        public virtual BaseForm CreateGhostForm(BaseForm sender, long formTypeID, FormMessage formMessage)
        {
            BaseForm CurrentForm = null;
            CurrentForm = CreateForm(formTypeID, formMessage);
            if (sender != null) CurrentForm.SenderGuid = sender.Guid;
            DicOfForms[CurrentForm.Guid] = CurrentForm;
            CurrentForm.Visible = false;
            return CurrentForm;
        }

        /// <summary>
        /// Создание невидимой формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="formTypeID"></param>
        /// <returns></returns>
        public virtual BaseForm CreateGhostForm(BaseForm sender, long formTypeID)
        {
            return CreateGhostForm(sender, formTypeID, null);
        }


        /// <summary>
        /// Создание и показ новой формы, старая не удаляется
        /// </summary>
        public virtual BaseForm ShowForm(BaseForm sender, long formTypeID)
        {
            return ShowForm(sender, formTypeID, null);
        }

        /// <summary>
        /// Убрать значение из справочника 
        /// </summary>
        /// <param name="deletedForm"></param>
        /// <returns></returns>
        public virtual int DeleteForm(BaseForm deletedForm)
        {
            if (deletedForm != null && Guid.Empty!=deletedForm.Guid)
            {
                if (IsContainForm(deletedForm.Guid))
                {
                    DicOfForms.Remove(deletedForm.Guid);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// отправка сообщения форме
        /// </summary>
        /// <param name="sender">посылатель сообщения</param>
        /// <param name="guidOfForm">получатель сообщения</param>
        /// <param name="formMessage">сообщение</param>
        /// <returns></returns>
        public virtual int SendMessageToForm(BaseForm sender, Guid guidOfForm, FormMessage formMessage)
        {
            if (IsContainForm(guidOfForm))
            {
                return DicOfForms[guidOfForm].ReceiveMessage(formMessage);
            }
            else
            {
                return -1;
            }
        }

        public virtual bool IsContainFormType(long myFormTypeID)
        {

            foreach (KeyValuePair<Guid, BaseForm> pair in DicOfForms)
            {
                if (((BaseForm)pair.Value).TypeID == myFormTypeID)
                    return true;
            }
            return false;
        }


        public BaseForm ShowSingletoneForm(BaseForm sender, long formTypeID, FormMessage formMessage)
        {
            var existForms = FindFormsByType(formTypeID);
            if (existForms.Count > 0)
            {
                SendMessageToForm(sender, existForms[0].Guid, formMessage);
                existForms[0].Focus();
                return existForms[0];
            }
            else
            {
                return ShowForm(sender, formTypeID, formMessage);
            }
        }

        public List<BaseForm> FindFormsByType(long typeID)
        {
            List<BaseForm> result = new List<BaseForm>();
            var keys = DicOfForms.Keys;
            foreach (var key in keys)
            {
                var item = DicOfForms[key];
                if (item != null && item.TypeID == typeID)
                    result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// Создание и показ новой формы, старая не удаляется
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="formTypeID"></param>
        /// <param name="formMessage"></param>
        /// <returns></returns>
        public virtual BaseForm ShowForm(BaseForm sender, long formTypeID, FormMessage formMessage)
        {
            BaseForm CurrentForm = null;
            CurrentForm = CreateForm(formTypeID, formMessage);
            if (sender != null) CurrentForm.SenderGuid = sender.Guid;
            DicOfForms[CurrentForm.Guid] = CurrentForm;
            MainApplicationContext.MainForm.Invoke(new MethodInvoker(() =>
            {
                CurrentForm.Show();
            }));
            return CurrentForm;
        }

        /// <summary>
        /// Получить форму из словаря
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public virtual BaseForm GetFormByGuid(Guid guid)
        {
            if (DicOfForms.ContainsKey(guid))
            {
                return DicOfForms[guid];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Содержит ли словарь форму?
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public virtual bool IsContainForm(Guid guid)
        {
            if (DicOfForms.ContainsKey(guid))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void RegisterForm(BaseForm myForm)
        {
            this.DicOfForms[myForm.Guid] = myForm;
        }
    }
}
