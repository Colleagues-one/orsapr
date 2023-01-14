using Kompas6API5;
using Kompas6Constants3D;
using System;

namespace ORSAPR.Model
{
    /// <summary>
    /// класс Менеджера
    /// </summary>
	public class Manager
    {
        /// <summary>
        /// выделение памяти под объект компас коннектора
        /// </summary>
		public KompasConnector _kompasApp = new KompasConnector();

        /// <summary>
        /// Конструктор с вложенным классом
        /// </summary>
        /// <param name="kompasApp"></param>
        public Manager(KompasConnector kompasApp)
        {
            if (kompasApp == null)
            {
                return;
            }
            _kompasApp = kompasApp;
        }

        /// <summary>
        /// функция сборки слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        public void BuildModelLocksmith(ChiselData chiselData)
        {
            var Chisel = _kompasApp.Chisel;
            TopLocksmith(chiselData, Chisel);
            InnerCutoutLocksmith(chiselData, Chisel);
            BladeChamferLocksmith(chiselData, Chisel);
            HandleFilletLocksmith(chiselData, Chisel);
        }

        /// <summary>
        /// функция сборки пикового зубила
        /// </summary>
        /// <param name="chiselData"></param>
        public void BuildModelPeak(ChiselData chiselData)
        {
            var Chisel = _kompasApp.Chisel;
            TopPeak(chiselData, Chisel);
            BladePeak(chiselData, Chisel);
            BladeChamferPeak(chiselData, Chisel);
            HandleFilletPeak(chiselData, Chisel);
        }

        /// <summary>
        /// функция расчета параметров для выреза слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        /// <param name="iDocument2D"></param>
        /// <returns></returns>
        private ksDocument2D CalculateSketchParamethers(ChiselData chiselData,
            ksDocument2D iDocument2D, double x0, double y0, double x1, double y1)
        {
            iDocument2D.ksLineSeg(x0, y0, x0, y1, 1);
            iDocument2D.ksLineSeg(x0, y1, x1, y1, 1);
            iDocument2D.ksLineSeg(x1, y1, x1, y0, 1);
            iDocument2D.ksLineSeg(x1, y0, x0, y0, 1);

            return iDocument2D;
        }
       
        /// <summary>
        /// функция создания фаски
        /// </summary>
        /// <param name="Chisel"></param>
        /// <param name="transfer"></param>
        /// <param name="distance1"></param>
        /// <param name="distance2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private void ChamferBuilder(ksPart Chisel, bool transfer, double distance1, double distance2,
            double x, double y, double z)
        {
            ksEntity iChamfer = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_chamfer);
            if (iChamfer != null)
            {
                ksChamferDefinition chamferDefinition =
                    (ksChamferDefinition)iChamfer.GetDefinition();
                if (chamferDefinition != null)
                {
                    chamferDefinition.tangent = true;
                    chamferDefinition.SetChamferParam(transfer, distance1,
                        distance2);
                    var ar = chamferDefinition.array();

                    EntityCollection iCollection = Chisel.
                        EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(x, y, z);
                    ar.Add(iCollection.Last());
                    iChamfer.Create();
                }
            }
        }

        /// <summary>
        /// функция построения основания зубила пики
        /// </summary>
        /// <param name="chiselData"></param>
        private void TopPeak(ChiselData chiselData, ksPart Chisel)
        {           
            // Эскиз ручки
            ksEntity planeXOZ = (ksEntity)Chisel.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            ksEntity SketchTop = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_sketch);
            if (SketchTop != null)
            {
                // интерфейс свойств эскиза
                ksSketchDefinition sketchDef = (ksSketchDefinition)SketchTop.GetDefinition();
                if (sketchDef != null)
                {
                    sketchDef.SetPlane(planeXOZ);
                    SketchTop.Create();

                    ksDocument2D sketchEdit2 = (ksDocument2D)sketchDef.BeginEdit();
                    sketchEdit2.ksCircle(0, 0, 0.5 * chiselData.Height, 1);
                    sketchDef.EndEdit();

                    // приклеим выдавливанием
                    ksEntity entityBossExtr =
                        (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                    if (entityBossExtr != null)
                    {
                        ksBossExtrusionDefinition bossExtrDef =
                            (ksBossExtrusionDefinition)entityBossExtr.GetDefinition();
                        if (bossExtrDef != null)
                        {
                            ksExtrusionParam extrProp =
                                (ksExtrusionParam)bossExtrDef.ExtrusionParam(); 

                            if (extrProp != null)
                            {
                                bossExtrDef.SetSketch(SketchTop); // эскиз операции выдавливания

                                extrProp.direction = (short)Direction_Type.dtBoth;
                                extrProp.typeNormal = (short)End_Type.etBlind;
                                extrProp.depthReverse = 0.5 * chiselData.Length;
                                extrProp.depthNormal = (0.5 * chiselData.Length)
                                    - (0.5 * chiselData.BladeLength);
                                entityBossExtr.Create();                // создадим операцию
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// функция построения лезвия зубила пики
        /// </summary>
        /// <param name="chiselData"></param>
        private void BladePeak(ChiselData chiselData, ksPart Chisel)
        {       
            //эскиз лезвия
            ksEntity SketchBlade = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_sketch);
            ksEntity ksEntityPlaneOffset =
                (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_planeOffset);

            ksEntity ksEntityPlaneXOZ =
                (ksEntity)Chisel.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            ksPlaneOffsetDefinition ksPlaneOffsetDefinition =
                (ksPlaneOffsetDefinition)ksEntityPlaneOffset.GetDefinition();
            ksPlaneOffsetDefinition.SetPlane(ksEntityPlaneXOZ);
            ksPlaneOffsetDefinition.direction = true;
            ksPlaneOffsetDefinition.offset = (0.5 * chiselData.Length) 
                - (0.5 * chiselData.BladeLength);
            ksEntityPlaneOffset.Create();

            if (SketchBlade != null)
            {
                // интерфейс свойств эскиза
                ksSketchDefinition sketchDef2 = (ksSketchDefinition)SketchBlade.GetDefinition();
                if (sketchDef2 != null)
                {
                    sketchDef2.SetPlane(ksEntityPlaneOffset);  // установим плоскость
                    SketchBlade.Create();         // создадим эскиз
                    ksDocument2D iDocument2D = (ksDocument2D)sketchDef2.BeginEdit();
                    //method Calculate

                    CalculateSketchParamethers(chiselData, iDocument2D,
                        (-0.5 * chiselData.Height),//x0
                        (-0.5 * chiselData.Width),//y0
                        (0.5 * chiselData.Height),//x1
                        (0.5 * chiselData.Width));//y1

                    sketchDef2.EndEdit();

                    ksEntity entityExtr =
                        (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                    if (entityExtr != null)
                    {
                        // интерфейс свойств базовой операции выдавливания
                        ksBossExtrusionDefinition extrusionDef =
                            (ksBossExtrusionDefinition)entityExtr.GetDefinition();

                        if (extrusionDef != null)
                        {
                            ksExtrusionParam extrProp =
                                (ksExtrusionParam)extrusionDef.ExtrusionParam();

                            if (extrProp != null)
                            {
                                extrusionDef.SetSketch(SketchBlade);

                                extrProp.direction = (short)Direction_Type.dtNormal;
                                extrProp.typeNormal = (short)End_Type.etBlind;
                                extrProp.depthNormal = 0.5 * chiselData.BladeLength;
                                entityExtr.Create();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// функция построения фасок зубила пики
        /// </summary>
        /// <param name="chiselData"></param>
        private void BladeChamferPeak(ChiselData chiselData, ksPart Chisel)
        {
            //фаска вверх к ручке
            ChamferBuilder(Chisel, true, 0.5 * chiselData.Height, 0.5 * chiselData.BladeLength, 0,
            (0.5 * chiselData.Length) - (0.5 * chiselData.BladeLength), 0.5 * chiselData.Height);
            //боковая фаска
            ChamferBuilder(Chisel, false, 0.5 * chiselData.BladeLength,
            chiselData.BladeLength / 40, 0, 0.5 * chiselData.Length, -0.5 * chiselData.Width);
            ChamferBuilder(Chisel, false, 0.5 * chiselData.BladeLength,            
            chiselData.BladeLength / 40, 0, 0.5 * chiselData.Length, 0.5 * chiselData.Width);
            // фаска сужения лезвия
            ChamferBuilder(Chisel, true, 0.5 * chiselData.Height * 0.7662626,
                chiselData.BladeLength, 0.5 * chiselData.Height, 0.5 * chiselData.Length, 0);
            ChamferBuilder(Chisel, true, 0.5 * chiselData.Height * 0.7662626,
               chiselData.BladeLength, -0.5 * chiselData.Height, 0.5 * chiselData.Length, 0);
            //фаска лезвия
            ChamferBuilder(Chisel, false, chiselData.BladeLength / 40, (0.7 / 3) * 0.5 * chiselData.Height,
            (0.5 * chiselData.Height)- (0.5 * chiselData.Height * 0.7662626), 0.5 * chiselData.Length, 0);
            ChamferBuilder(Chisel, false, chiselData.BladeLength / 40, (0.7 / 3) * 0.5 * chiselData.Height,
            -0.5 * chiselData.Height + (0.5 * chiselData.Height * 0.7662626), 0.5 * chiselData.Length, 0);          
        }

        


        /// <summary>
        /// функция построения скругления зубила пики
        /// </summary>
        /// <param name="chiselData"></param>
        private void HandleFilletPeak(ChiselData chiselData, ksPart Chisel)
        {
            /// Создание скругления
            ksEntity iFillet = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_fillet);
            if (iFillet != null)
            {
                ksFilletDefinition filletDefenition = (ksFilletDefinition)iFillet.GetDefinition();
                if (filletDefenition != null)
                {
                    filletDefenition.radius = 0.5 * chiselData.Height / 3;
                    filletDefenition.tangent = true;
                    var araa = filletDefenition.array();

                    //сзади
                    EntityCollection iCollection =
                        Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1, -0.5 * chiselData.Length, 1);
                    araa.Add(iCollection.Last());
                    iFillet.Create();
                }
            }
        }

        /// <summary>
        /// функция построения основания слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        private void TopLocksmith(ChiselData chiselData, ksPart Chisel)
        {
            // получим интерфейс базовой плоскости XOY
            ksEntity planeXOY = (ksEntity)Chisel.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            ksEntity iSketch = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_sketch);
            if (iSketch != null)
            {
                // интерфейс свойств эскиза
                ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
                if (iDefinitionSketch != null)
                {
                    iDefinitionSketch.SetPlane(planeXOY);
                    iSketch.Create();
                    ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();

                    CalculateSketchParamethers(chiselData, iDocument2D,
                      (-0.5 * chiselData.Width),//x0
                       (-0.5 * chiselData.Length),//y0
                        (0.5 * chiselData.Width),//x1
                        (0.5 * chiselData.Length));//y1

                    iDefinitionSketch.EndEdit();

                    ksEntity entityExtr = (ksEntity)Chisel.
                        NewEntity((short)Obj3dType.o3d_bossExtrusion);
                    if (entityExtr != null)
                    {
                        // интерфейс свойств базовой операции выдавливания
                        // интерфейс базовой операции выдавливания
                        ksBossExtrusionDefinition extrusionDef =
                            (ksBossExtrusionDefinition)entityExtr.GetDefinition();
                        if (extrusionDef != null)
                        {// интерфейс структуры параметров выдавливания
                            ksExtrusionParam extrProp =
                                (ksExtrusionParam)extrusionDef.ExtrusionParam();
                            if (extrProp != null)
                            {
                                extrusionDef.SetSketch(iSketch); // эскиз операции выдавливания
                                // направление выдавливания (прямое)
                                extrProp.direction = (short)Direction_Type.dtMiddlePlane;
                                // тип выдавливания (строго на глубину)
                                extrProp.typeNormal = (short)End_Type.etBlind;
                                // глубина выдавливания    
                                extrProp.depthReverse = chiselData.Height;
                                // создадим операцию
                                entityExtr.Create();                             
                            }                         
                        }
                    }
                }                
            }
        }
  
        /// <summary>
        /// функция построения внутреннего выреза слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        private void InnerCutoutLocksmith(ChiselData chiselData, ksPart Chisel)
        {
            // создадим новый эскиз         
            ksEntity entitySketch2 = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_sketch);
            ksEntity ksEntityPlaneOffset =
                (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_planeOffset);

            ksEntity ksEntityPlaneXOY =
                (ksEntity)Chisel.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            ksPlaneOffsetDefinition ksPlaneOffsetDefinition =
                (ksPlaneOffsetDefinition)ksEntityPlaneOffset.GetDefinition();
            ksPlaneOffsetDefinition.SetPlane(ksEntityPlaneXOY);
            ksPlaneOffsetDefinition.direction = true;
            ksPlaneOffsetDefinition.offset = 0.5 * chiselData.Height;
            ksEntityPlaneOffset.Create();

            if (entitySketch2 != null)
            {
                // интерфейс свойств эскиза
                ksSketchDefinition sketchDef2 = (ksSketchDefinition)entitySketch2.GetDefinition();
                if (sketchDef2 != null)
                {                  
                    sketchDef2.SetPlane(ksEntityPlaneOffset);  // установим плоскость
                    entitySketch2.Create();         // создадим эскиз
                    ksDocument2D iDocument2D = (ksDocument2D)sketchDef2.BeginEdit();

                    CalculateSketchParamethers(chiselData, iDocument2D,
                        (-(chiselData.Width / 2) + (0.5 * (chiselData.Width - chiselData.InnerWidth))),//x0
                        (- ((0.5 * chiselData.Length) - (0.15 * chiselData.Length))),//y0
                       ( 0.5 * chiselData.Width - (0.5 * (chiselData.Width - chiselData.InnerWidth))),//x1
                       ( - (0.5 * chiselData.Length) + (0.15 * chiselData.Length) + chiselData.InnerLength));//y1

                    sketchDef2.EndEdit();

                    ksEntity entityExtr =
                        (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_cutExtrusion);
                    if (entityExtr != null)
                    {
                        // интерфейс свойств базовой операции выдавливания
                        ksCutExtrusionDefinition extrusionDef =
                            (ksCutExtrusionDefinition)entityExtr.GetDefinition(); 

                        if (extrusionDef != null)
                        {
                            ksExtrusionParam extrProp = 
                                (ksExtrusionParam)extrusionDef.ExtrusionParam();

                            if (extrProp != null)
                            {
                                extrusionDef.SetSketch(entitySketch2); 

                                extrProp.direction = (short)Direction_Type.dtNormal;    
                                extrProp.typeNormal = (short)End_Type.etBlind;    
                                extrProp.depthNormal = chiselData.Height / 6;          
                                entityExtr.Create();                
                            }
                        }
                    }
                }
            }
        }

       /// <summary>
       /// функция построения фаски лезвия слесарного зубила
       /// </summary>
       /// <param name="chiselData"></param>
        private void BladeChamferLocksmith(ChiselData chiselData, ksPart Chisel)
        {         
            //боковые фаски
            ChamferBuilder(Chisel, false, chiselData.BladeLength, chiselData.BladeLength / 40,
                -0.5 * chiselData.Width, 0.5 * chiselData.Length, 1);
            ChamferBuilder(Chisel, true, chiselData.BladeLength, chiselData.BladeLength / 40,
                0.5 * chiselData.Width, 0.5 * chiselData.Length, 1);
            //фаска сужения лезвия
            ChamferBuilder(Chisel, false, 0.5 * chiselData.Height * 0.7662626, chiselData.BladeLength, 0,
            0.5 * chiselData.Length, 0.5 * chiselData.Height);
            ChamferBuilder(Chisel, true, 0.5 * chiselData.Height * 0.7662626, chiselData.BladeLength, 0,
            0.5 * chiselData.Length, -0.5 * chiselData.Height);
            //фаска лезвия
            ChamferBuilder(Chisel, true, chiselData.BladeLength / 40, (0.7 / 3) * chiselData.Height / 2,
            0, 0.5 * chiselData.Length, (0.5 * chiselData.Height) - (0.5 * chiselData.Height * 0.7662626));
            ChamferBuilder(Chisel, false, chiselData.BladeLength / 40, (0.7 / 3) * chiselData.Height / 2,
            0, 0.5 * chiselData.Length,-(0.5 * chiselData.Height) + (0.5 * chiselData.Height * 0.7662626));
        }
      
        /// <summary>
        /// сборщик скруглений слесарного зубила
        /// </summary>
        /// <param name="chiselData"></param>
        private void HandleFilletLocksmith(ChiselData chiselData, ksPart Chisel)
        {
            /// Создание скругления
            ksEntity iFillet = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_fillet);
            if (iFillet != null)
            {
                ksFilletDefinition filletDefenition = (ksFilletDefinition)iFillet.GetDefinition();
                if (filletDefenition != null)
                {
                    filletDefenition.radius = chiselData.Height / 6;
                    filletDefenition.tangent = true;
                    var araa = filletDefenition.array();

                    //сзади
                    EntityCollection iCollection =
                        Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1, -chiselData.Length / 2, 1);
                    araa.Add(iCollection.Last());

                    //ребра
                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(-(chiselData.Width / 2),
                        -0.5 * chiselData.Length + 10, 0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(chiselData.Width / 2,
                        -0.5 * chiselData.Length + 10, 0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(-(chiselData.Width / 2),
                        -0.5 * chiselData.Length + 10, -0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_edge);
                    iCollection.SelectByPoint(chiselData.Width / 2,
                        -0.5 * chiselData.Length + 10, -0.5 * chiselData.Height);
                    araa.Add(iCollection.Last());

                    iFillet.Create();
                }
            }
             ksEntity iFilletInner = (ksEntity)Chisel.NewEntity((short)Obj3dType.o3d_fillet);
            if (iFilletInner != null)
            {
                ksFilletDefinition filletInnerDefenition = (ksFilletDefinition)iFilletInner.GetDefinition();
                if (filletInnerDefenition != null)
                {
                    double radius;
                    if(0.5*chiselData.InnerWidth <= chiselData.Height/12)
                    {
                        radius = 0.5 * chiselData.InnerWidth;
                    }
                    else
                    {
                        radius= chiselData.Height / 12;
                    }
                    filletInnerDefenition.radius = radius;
                    filletInnerDefenition.tangent = true;
                    var araa = filletInnerDefenition.array();
                    //вырез
                    EntityCollection iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(
                       -(0.5 * chiselData.Width) + (0.5 * (chiselData.Width - chiselData.InnerWidth)),
                       -(0.5 * chiselData.Length) + (0.17 * chiselData.Length),
                         0.5 * chiselData.Height - 0.1);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(
                        0.5 * chiselData.Width - (0.5 * (chiselData.Width - chiselData.InnerWidth)),
                      -(0.5 * chiselData.Length) + (0.17 * chiselData.Length),
                        0.5 * chiselData.Height - 0.1);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1,
                        -(0.5 * chiselData.Length) + (0.15 * chiselData.Length) + chiselData.InnerLength,
                        0.5 * chiselData.Height - 0.1);
                    araa.Add(iCollection.Last());

                    iCollection = Chisel.EntityCollection((short)Obj3dType.o3d_face);
                    iCollection.SelectByPoint(1,
                        -((0.5 * chiselData.Length) - (0.15 * chiselData.Length)),
                        0.5 * chiselData.Height - 0.1);
                    araa.Add(iCollection.Last());

                    iFilletInner.Create();
                }

            }
        } 
    }
}

