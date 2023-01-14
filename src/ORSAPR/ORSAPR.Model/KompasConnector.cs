using System;
using Kompas6API5;
using Kompas6Constants3D;
using System.Runtime.InteropServices;

namespace ORSAPR.Model
{
    /// <summary>
    /// класс подключения компаса
    /// </summary>
    public class KompasConnector
    {
        /// <summary>
        /// объект компаса
        /// </summary>
        private KompasObject KompasObject
        {
            get;
            set;
        }

        /// <summary>
        /// объект документа
        /// </summary>
        private ksDocument3D Document3D
        {
            get;
            set;
        }     

        /// <summary>
        /// поле детали
        /// </summary>
        public ksPart Chisel
        {
            get;
            private set;
        }

        /// <summary>
        /// конструктор класса подключения компаса
        /// </summary>
        public KompasConnector()
        {
            if (!GetActiveApp())
            {
                if (!CreateNewApp())
                {               
                    return;                   
                }
            }
        }

        /// <summary>
        /// метод создания документа 3Д
        /// </summary>
        /// <returns></returns>
        public bool CreateDocument3D()
        {          
            Document3D = (ksDocument3D)KompasObject.Document3D();           

            if (!Document3D.Create(false/*visible*/, true/*build*/))
            {
                return false;
            }
           
            Chisel = (ksPart)Document3D.GetPart((short)Part_Type.pTop_Part);
           
            if (Chisel == null)
            {
                return false;
            }
            return true;
        }     

        /// <summary>
        /// метод активации прилложения компас
        /// </summary>
        /// <returns></returns>
		private bool GetActiveApp()
		{		
			if (KompasObject == null)
			{
				try
				{
					KompasObject = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
				}
				catch
				{
					return false;
				}
			}
	
			if (KompasObject == null)
			{
				return false;
			}

			KompasObject.Visible = true;
			KompasObject.ActivateControllerAPI();

			return true;
		}

        /// <summary>
        /// метод запуска приложения компас
        /// </summary>
        /// <returns></returns>
		private bool CreateNewApp()
		{
			Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
			KompasObject = (KompasObject)Activator.CreateInstance(t);

			if (KompasObject == null)
			{			
				return false;
			}

			KompasObject.Visible = true;
			KompasObject.ActivateControllerAPI();
            
            return true;
		}

        /// <summary>
        /// метод уничтожения приложения компас
        /// </summary>
		public void DestructApp()
        {  
            KompasObject.Quit();
            Chisel = null;
            Document3D = null;           
            KompasObject = null; 
        }
	}
}

