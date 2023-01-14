using System;

namespace ORSAPR.Model
{
    /// <summary>
    /// класс параметров зубила
    /// </summary>
    public class ChiselData
    {
        /// <summary>
        /// поле ширины зубила
        /// </summary>
        private double _width;

        /// <summary>
        /// поле длины зубила
        /// </summary>
        private double _length;

        /// <summary>
        /// поле высоты зубила
        /// </summary>
        private double _height;

        /// <summary>
        /// поле длины лезвия зубила
        /// </summary>
        private double _bladeLength;

        /// <summary>
        /// поле длины внутреннего выреза зубила
        /// </summary>
        private double _innerLength;

        /// <summary>
        /// поле ширины внутреннего выреза зубила
        /// </summary>
        private double _innerWidth;

        /// <summary>
        /// возвращает или задает ширину
        /// </summary>
        public double Width
        {
            get => _width;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Invalid chisel width range." +
                        $"{ Environment.NewLine} Сannot be width = 0!");
                }
                if (value < 10 || value > 30)
                {
                    throw new ArgumentException("Invalid chisel width range," +
                        $"{ Environment.NewLine} please check the entered " +
                        $"{ Environment.NewLine} parameters according to the" +
                        $"{ Environment.NewLine} range: 10mm <= W <= 30mm.");
                }
                _width = value;
            }
        }

        /// <summary>
        /// возвращает или задает длину
        /// </summary>
        public double Length
        {
            get => _length;
            set
            {               
                if (value == 0)
                {                 
                    throw new ArgumentException("Invalid chisel lenght range." +
                        $"{ Environment.NewLine}Сannot be length = 0!");
                }              
                if (_width != 0)
                {
                    if(_width >= 10 && _width <= 30)
                    {
                       
                        if (value < 10 * _width || value >= 10 * _width + 10
                            || value < 100 || value > 300)
                        {
                            if (_width == 30)
                            {
                                throw new ArgumentException($"Invalid chisel lenght range," +
                                   $"{ Environment.NewLine} please check the entered" +
                                   $"{ Environment.NewLine} parameters according to the" +
                                   $"{ Environment.NewLine} range: {10 * _width}mm <= L <= 300mm.");
                            }
                            if (_width >= 29)
                            {
                                throw new ArgumentException($"Invalid chisel lenght range," +
                                    $"{ Environment.NewLine} please check the entered" +
                                    $"{ Environment.NewLine} parameters according to the" +
                                    $"{ Environment.NewLine} range: {10 * _width}mm <= L < 300mm.");
                            }
                            else
                            throw new ArgumentException($"Invalid chisel lenght range," +
                              $"{ Environment.NewLine} please check the entered" +
                              $"{ Environment.NewLine} parameters according to the" +
                              $"{Environment.NewLine} range: {10 * _width}mm <= L < {10 * _width + 10}mm.");
                        }        
                    }                   
                }
                if (value < 100 || value > 300)
                {
                    throw new ArgumentException("Invalid chisel lenght range," +
                        $"{Environment.NewLine} please check the entered" +
                        $"{Environment.NewLine} parameters according to the" +
                        $"{Environment.NewLine} range: 100mm <= L <= 300mm.");
                }
                _length = value;
            }
        }

        /// <summary>
        /// возвращает или задает высоту
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                if (value == 0)
                {                   
                    throw new ArgumentException("Invalid chisel height range." +
                        $"{Environment.NewLine} Сannot be height = 0!");
                }         
                if (_width != 0)
                {
                    if (_width >= 10 && _width <= 30)
                    {
                        if (value < 0.6 * _width || value > 0.8 * _width)
                        {
                            throw new ArgumentException($"Invalid chisel height range," +
                                $"{Environment.NewLine} please check the entered" +
                                $"{Environment.NewLine} parameters according to the" +
                                $"{Environment.NewLine} range: {0.6 * _width}mm <= H <= {0.8 * _width}mm.");
                        }
                    }                   
                }
                else if (value < 6 || value > 24)
                {
                    throw new ArgumentException("Invalid chisel height range," +
                        $"{Environment.NewLine} please check the entered" +
                        $"{Environment.NewLine} parameters according to the" +
                        $"{Environment.NewLine} range: 6mm <= H <= 24mm.");
                }
                _height = value;                                          
            }
        }

        /// <summary>
        /// возвращает или задает длину лезвия зубила
        /// </summary>
        public double BladeLength
        {
            get => _bladeLength;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Invalid chisel blade lenght range. " +
                        $"{Environment.NewLine}Сannot be blade Lenght = 0!");
                }            
                if (_length != 0)
                {
                    if(_length >= 100 && _length <= 300)
                    {
                        if (value < 0.4 * _length || value > 0.5 * _length)
                        {
                            throw new ArgumentException($"Invalid chisel blade lenght range," +
                              $"{Environment.NewLine} please check the entered" +
                              $"{Environment.NewLine} parameters according to the" +
                              $"{Environment.NewLine} range: {0.4 * _length}mm <= l1 <= {0.5 * _length}mm.");
                        }
                    }                    
                }
                else if (value < 40 || value > 150)
                {
                    throw new ArgumentException("Invalid chisel blade lenght range," +
                        $"{Environment.NewLine} please check the entered" +
                        $"{Environment.NewLine} parameters according to the" +
                        $"{Environment.NewLine} range: 40mm <= l1 <= 150mm.");
                }
                _bladeLength = value;
            }
        }

        /// <summary>
        /// возвращает или задает длину внутреннего выреза зубила
        /// </summary>
        public double InnerLength
        {
            get => _innerLength;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Invalid chisel inner cotout lenght range." +
                        $"{Environment.NewLine} Сannot be inner cotout lenght = 0!");
                }              
                if (_length != 0)
                {
                    if(_length >= 100 && _length <= 300)
                    {
                        if (value < 10 || value > 0.25 * _length)
                        {
                            throw new ArgumentException($"Invalid chisel inner cotout" +
                                $"{Environment.NewLine} lenght range, please check the" +
                                $"{Environment.NewLine} entered parameters according to the" +
                                $"{Environment.NewLine} range: 10mm <= l2 <= {0.25 * _length}mm.");
                        }
                    }                   
                }
                else if (value < 10 || value > 75)
                {
                    throw new ArgumentException("Invalid chisel inner cotout" +
                        $"{Environment.NewLine}lenght range, please check" +
                        $"{Environment.NewLine} the entered parameters according" +
                        $"{Environment.NewLine} to the range: 10mm <= l2 <= 75mm.");
                }
                _innerLength = value;
            }
        }

        /// <summary>
        /// возвращает или задает ширину внутреннего выреза зубила
        /// </summary>
        public double InnerWidth
        {
            get => _innerWidth;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Invalid chisel inner cotout width range." +
                        $"{Environment.NewLine} Сannot be inner cotout lenght = 0!");
                }               
                if (_width != 0 && _innerLength == 0)
                {
                    if (_width >= 10 && _width <= 30)
                    {
                        if (value < 5 || value > 0.5 * _width)
                        {
                            throw new ArgumentException($"Invalid chisel inner cotout width" +
                                $"{Environment.NewLine} range, please check the entered" +
                                $"{Environment.NewLine} parameters according to the" +
                                $"{Environment.NewLine} range: 5mm <= w1 <= {0.5 * _width}mm.");                      
                        }
                    }                   
                }
                if (_width == 0 && _innerLength != 0)
                {
                    if (_innerLength >= 10 && _innerLength <= 75)
                    {
                        if (value < 5 || value > _innerLength * 0.5)
                        {
                            throw new ArgumentException($"Invalid chisel inner cotout width" +
                                $"{Environment.NewLine} range, please check the entered" +
                                $"{Environment.NewLine} parameters according to the" +
                                $"{Environment.NewLine} range: 5mm <= w1 <= {_innerLength * 0.5}mm.");
                        }
                    }
                }
                if(_width != 0 && _innerLength != 0)
                {
                    if (_innerLength >= 10 && _innerLength <= 75 && _width >= 10 && _width <= 30)
                    {
                        if (_innerLength > _width)
                        {
                            if (value < 5 || value > 0.5 * _width)
                            {
                                throw new ArgumentException($"Invalid chisel inner cotout width" +
                                    $"{Environment.NewLine} range, please check the entered" +
                                    $"{Environment.NewLine} parameters according to the" +
                                    $"{Environment.NewLine} range: 5mm <= w1 <= {0.5 * _width}mm.");
                            }
                        }                      
                        else
                        {
                            if (value < 5 || value > _innerLength * 0.5)
                            {
                                throw new ArgumentException($"Invalid chisel inner cotout width" +
                                    $"{Environment.NewLine} range, please check the entered" +
                                    $"{Environment.NewLine} parameters according to the" +
                                    $"{Environment.NewLine} range: 5mm <= w1 <= {_innerLength * 0.5}mm.");
                            }
                        }
                    }
                    if((_innerLength < 10 || _innerLength > 75) && (_width >= 10 && _width <= 30))
                    {
                        if (value < 5 || value > _innerLength * 0.5)
                        {
                            throw new ArgumentException($"Invalid chisel inner cotout width" +
                                $"{Environment.NewLine} range, please check the entered" +
                                $"{Environment.NewLine} parameters according to the" +
                                $"{Environment.NewLine} range: 5mm <= w1 <= {_innerLength * 0.5}mm.");
                        }
                    }
                    if ((_innerLength >= 10 && _innerLength <= 75) && (_width < 10 || _width > 30))
                    {
                        if (value < 5 || value > 0.5 * _width)
                        {
                            throw new ArgumentException($"Invalid chisel inner cotout width" +
                                $"{Environment.NewLine} range, please check the entered" +
                                $"{Environment.NewLine} parameters according to the" +
                                $"{Environment.NewLine} range: 5mm <= w1 <= {0.5 * _width}mm.");
                        }
                    }
                    if ((_innerLength < 10 || _innerLength > 75) && (_width < 10 || _width > 30))
                    {
                        throw new ArgumentException("Invalid chisel inner cotout" +
                            $"{Environment.NewLine} width range, please check the" +
                            $"{Environment.NewLine} entered parameters according" +
                        $"{Environment.NewLine} to the range: 5mm <= w1 <= 15mm.");
                    }
                }
                else if (value < 5 || value > 15)
                {
                    throw new ArgumentException("Invalid chisel inner cotout" +
                        $"{Environment.NewLine} width range, please check" +
                        $"{Environment.NewLine} the entered parameters according" +
                       $"{Environment.NewLine} to the range: 5mm <= w1 <= 15mm.");
                }
                _innerWidth = value;
            }
        }
    }
}
