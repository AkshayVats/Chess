using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        Dictionary<ChessPiece, Image> _imageMap = new Dictionary<ChessPiece, Image>();

        private Image _lastSelected;
        private ITurnManager _turnManager;

        public BoardUi(Canvas canvas)
        {
            _canvas = canvas;
            DrawBoxes();
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
                    rec.MouseDown += Box_MouseDown;
                    rec.Tag = new Point(i,j);
                }

            _boardObjCnt = _canvas.Children.Count;
        }

        /// <summary>
        /// Renders pieces
        /// </summary>
        /// <param name="board">state of chess board</param>
        public void Render(ChessPiece[,] board)
        {
            //TODO: Clear lit boxes
            if(_canvas.Children.Count>_boardObjCnt)
            {
                _canvas.Children.RemoveRange(_boardObjCnt, _canvas.Children.Count - _boardObjCnt);
            }
            _lastSelected = null;
            _imageMap.Clear();
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
                        Cursor = Cursors.Hand,
                        Effect = new DropShadowEffect() { Color = Colors.White, ShadowDepth = 0}    //makes black pieces visible on black squares
                    };
                    //hook up the mouse down event
                    img.Tag = piece;
                    _imageMap[piece] = img;
                    img.MouseDown += Icon_MouseDown;
                    _canvas.Children.Add(img);
                    Canvas.SetLeft(img, sideLineSz + (j+0.5-iconRatio/2) * _sqSz);
                    Canvas.SetTop(img, sideLineSz + (i + 0.5 - iconRatio / 2) * _sqSz);
                }
        }

        public void SetUiManager(ITurnManager turnManager)
        {
            _turnManager = turnManager;
        }

        private void Box_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //User clicks a box
        }

        private void Icon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _turnManager?.IconClicked((sender as Image).Tag as ChessPiece);
        }

        public void SelectIcon(ChessPiece piece)
        {
            Image icon;
            if (_imageMap.TryGetValue(piece, out icon))
            {
                if (_lastSelected != null)
                {
                    var ef = (_lastSelected.Effect as DropShadowEffect);
                    ef.Color = Colors.White;
                    ef.BlurRadius = 5;
                }
                _lastSelected = icon;
                var effect = (_lastSelected.Effect as DropShadowEffect);
                effect.Color = Colors.Red;
                effect.BlurRadius = 20;
            }
        }

        public void LightCells(IEnumerable<GridCell> cells)
        {
            //TODO: clear previously lit cells and light the cells
        }
        
    }
}
