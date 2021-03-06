using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using Brushes = System.Windows.Media.Brushes;

namespace XSemmel.Schema.Parser {

    public class XsdElement : XsdNode, IXsdHasName, IXsdHasRef, IXsdHasType, IXsdCountable, IXsdHasAttribute
    {

        public XsdElement(XmlNode node) : base(node)
        {
            {
                string attr = VisualizerHelper.GetAttr(node, "name");
                if (attr != null)
                {
                    Name = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "abstract");
                if (attr != null)
                {
                    Abstract = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "block");
                if (attr != null)
                {
                    Block = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "default");
                if (attr != null)
                {
                    Default = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "final");
                if (attr != null)
                {
                    Final = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "fixed");
                if (attr != null)
                {
                    Fixed = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "form");
                if (attr != null)
                {
                    Form = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "id");
                if (attr != null)
                {
                    Id = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "minOccurs");
                if (attr != null)
                {
                    MinOccurs = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "maxOccurs");
                if (attr != null)
                {
                    MaxOccurs = attr;
                }
            }{
                string attr = VisualizerHelper.GetAttr(node, "nillable");
                if (attr != null)
                {
                    Nillable = attr;
                }
            }{
                string attr = VisualizerHelper.GetAttr(node, "ref");
                if (attr != null)
                {
                    RefName = attr;
                }
            }{
                string attr = VisualizerHelper.GetAttr(node, "substitutionGroup");
                if (attr != null)
                {
                    SubstitutionGroup = attr;
                }
            }{
                string attr = VisualizerHelper.GetAttr(node, "type");
                if (attr != null)
                {
                    TypeName = attr;
                }
            }
        }

        public string Abstract { get; set; }
        public string Block { get; set; }
        public string Default { get; set; }
        public string Final { get; set; }
        public string Fixed { get; set; }
        public string Form { get; set; }
        public string Id { get; set; }
        public string MinOccurs { get; set; }
        public string MaxOccurs { get; set; }
        public string Nillable { get; set; }
        public string RefName { get; set; }
        public IXsdNode RefTarget { get; set; }
        public string SubstitutionGroup { get; set; }
        public string TypeName { get; set; }
        public IXsdNode TypeTarget { get; set; }

        private readonly HashSet<XsdAttribute> _attrs = new HashSet<XsdAttribute>();


        public void AddAtts(XsdAttribute attr)
        {
            _attrs.Add(attr);
        }

        public ICollection<XsdAttribute> GetAttributes()
        {
            return _attrs;
        }

        public override string ToString()
        {
            StringBuilder result;
            if (string.IsNullOrEmpty(Name))
            {
                if (string.IsNullOrEmpty(RefName))
                {
                    result = new StringBuilder("(none)");
                }
                else
                {
                    result = new StringBuilder("-> ");
                    result.Append(RefName);
                }
            }
            else
            {
                result = new StringBuilder(Name);    
            }
            
            VisualizerHelper.ToStringCountable(this, result);
            return result.ToString();
        }

        public override UIElement GetPaintComponent(XsdVisualizer.PaintOptions po, int fontSize)
        {
            StackPanel pnl = new StackPanel();
            if (fontSize <= 0) return pnl;

            pnl.Children.Add(GetPaintTitle(po, fontSize));

            if (_attrs.Count == 0)
            {
                pnl.Children.Add(new TextBlock
                {
                    Text = "(no attributes)",
                    Foreground = Brushes.Gray,
                    FontSize = fontSize
                });
            }
            else
            {
                foreach (XsdAttribute attr in _attrs)
                {
                    pnl.Children.Add(attr.GetPaintComponent(po, fontSize));
                }
            }

            pnl.Children.Add(GetPaintChildren(po, fontSize - 1));

            addMouseEvents(po, pnl, this);

            return new Border
                       {
                           BorderBrush = Brushes.Black, BorderThickness = new Thickness(1), Child = pnl,
                           Margin = new Thickness(5)
                       };
        }


        protected override UIElement GetPaintTitle(XsdVisualizer.PaintOptions po, int fontSize)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return base.GetPaintTitle(po, fontSize);
            }

            //Ref
            return new TextBlock
            {
                Text = ToString(),
                Background = new LinearGradientBrush(Colors.Turquoise, Colors.Transparent, 90),
                FontSize = fontSize,
                FontWeight = FontWeights.DemiBold
            };
        }

    }
}