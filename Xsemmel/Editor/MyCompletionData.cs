﻿using System;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System.Windows.Media;

namespace XSemmel.Editor
{
	/// <summary>
	/// Implements AvalonEdit ICompletionData interface to provide the entries in the completion drop down.
	/// </summary>
	public class MyCompletionData : ICompletionData
	{

		public MyCompletionData(string text) : this(text, null)
		{
		}

        public MyCompletionData(string text, string description)
        {
            Text = text;
            Content = Text;
            Description = description;
        }

        public MyCompletionData(string textToShow, string textToInsert, string description)
        {
            Text = textToInsert;
            Content = textToShow;
            Description = description;
        }

		public ImageSource Image 
        {
			get { return null; }
		}
		
		public string Text 
        { 
            get; 
            private set; 
        }
		
		// Use this property if you want to show a fancy UIElement in the drop down list.
        public object Content 
        { 
            get;
            private set;
        }

	    public object Description
        { 
            get;
            private set;
        }
		
		public double Priority { get { return 0; } }
		
		public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
		{
			textArea.Document.Replace(completionSegment, this.Text);
		}
	}
}
