using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess
{
    class BoardUi
    {
        private Canvas _canvas;

        public BoardUi(Canvas canvas)
        {
            _canvas = canvas;
            DrawBoxes();
        }

        private void DrawBoxes()
        {
            var width = _canvas.RenderSize.Width;
            var sideLineSz = 50.0;
            var sqSz = (width-2*sideLineSz)/8.0;
            char org = 'a';

            var outerRec = new Border()
            {
                BorderBrush = new SolidColorBrush(Colors.Black),
                Width = sqSz*8+2,
                Height = sqSz*8+2,
                BorderThickness = new Thickness(1)
            };
            _canvas.Children.Add(outerRec);
            Canvas.SetTop(outerRec, sideLineSz-1);
            Canvas.SetLeft(outerRec, sideLineSz-1);
            
            for (int i = 0; i < 8; i++)
            {
                var lh = new Label() {Content = ((char)(org+i)).ToString()};
                _canvas.Children.Add(lh);
                Canvas.SetTop(lh, sideLineSz/2);
                Canvas.SetLeft(lh, sideLineSz+sqSz / 2+i* sqSz);

                var lv = new Label() { Content = (i+1).ToString() };
                _canvas.Children.Add(lv);
                Canvas.SetTop(lv, sideLineSz+sqSz / 2 + i * sqSz);
                Canvas.SetLeft(lv, sideLineSz/2);
            }
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var rec = new Rectangle()
                    {
                        Fill = new SolidColorBrush((i+j)%2==0?Colors.White:Colors.Black),
                        Height = sqSz,
                        Width = sqSz
                    };
                    _canvas.Children.Add(rec);
                    Canvas.SetLeft(rec, sideLineSz+i*sqSz);
                    Canvas.SetTop(rec, sideLineSz + j * sqSz);
                }
        }
    }
}
