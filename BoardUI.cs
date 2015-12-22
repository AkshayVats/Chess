using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess
{
    class BoardUi
    {
        private int _boardObjCnt;
        private Canvas _canvas;
        private double sideLineSz = 50.0;       //width of side bar for labelling boxes
        private double _sqSz;
        private double iconRatio = 0.6;         //Size ratio of piece icon to box size
        //to cache icons
        private Dictionary<string, BitmapSource> _icons = new Dictionary<string, BitmapSource>();

        public BoardUi(Canvas canvas)
        {
            _canvas = canvas;
            DrawBoxes();
        }

        private void DrawBoxes()
        {
            var width = _canvas.RenderSize.Width;             
            _sqSz = (width-2*sideLineSz)/8.0;         //width and height of each square
            char org = 'a';                           //starting character of horizontal labelling

            //outer border of the main chess board excluding side lines
            var outerRec = new Border()
            {
                BorderBrush = new SolidColorBrush(Colors.Black),
                Width = _sqSz*8+2,
                Height = _sqSz*8+2,
                BorderThickness = new Thickness(1)
            };
            _canvas.Children.Add(outerRec);
            Canvas.SetTop(outerRec, sideLineSz-1);
            Canvas.SetLeft(outerRec, sideLineSz-1);
            

            //side line labelling
            for (int i = 0; i < 8; i++)
            {
                var lh = new Label() {Content = ((char)(org+i)).ToString()};
                _canvas.Children.Add(lh);
                Canvas.SetTop(lh, sideLineSz/2);
                Canvas.SetLeft(lh, sideLineSz+_sqSz / 2+i* _sqSz);

                var lv = new Label() { Content = (i+1).ToString() };
                _canvas.Children.Add(lv);
                Canvas.SetTop(lv, sideLineSz+_sqSz / 2 + i * _sqSz);
                Canvas.SetLeft(lv, sideLineSz/2);
            }

            //individual boxes
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    var rec = new Rectangle()
                    {
                        Fill = new SolidColorBrush((i+j)%2==0?Colors.White:Colors.Black),
                        Height = _sqSz,
                        Width = _sqSz
                    };
                    _canvas.Children.Add(rec);
                    Canvas.SetLeft(rec, sideLineSz+i*_sqSz);
                    Canvas.SetTop(rec, sideLineSz + j * _sqSz);
                }

            _boardObjCnt = _canvas.Children.Count;
        }

        /// <summary>
        /// Renders pieces
        /// </summary>
        /// <param name="board">state of chess board</param>
        public void Render(ChessPiece[,] board)
        {
            if(_canvas.Children.Count>_boardObjCnt)
            {
                _canvas.Children.RemoveRange(_boardObjCnt, _canvas.Children.Count - _boardObjCnt);
            }
            for(int i=0;i<8;i++)
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == null) continue;
                    var piece = board[i, j];
                    var img = new Image()
                    {
                        Height = _sqSz*iconRatio,
                        Width = _sqSz*iconRatio,
                        Source = GetIcon(piece.GetIcon()),
                        Effect = new DropShadowEffect() { Color = Colors.White, ShadowDepth = 0}    //makes black pieces visible on black squares
                };
                    _canvas.Children.Add(img);
                    Canvas.SetLeft(img, sideLineSz + (j+0.5-iconRatio/2) * _sqSz);
                    Canvas.SetTop(img, sideLineSz + (i + 0.5 - iconRatio / 2) * _sqSz);
                }
        }

        //for cache purposes
        private ImageSource GetIcon(string iconName)
        {
            BitmapSource src;
            if (!_icons.TryGetValue(iconName, out src))//TRY to find icon in MAP or dictionary
            {
                src = new BitmapImage(new Uri("pack://application:,,,/Images/" + iconName));
                _icons[iconName] = src;
            }
            return src;
        }
    }
}
