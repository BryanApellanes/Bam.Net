using System;
using System.Collections.Generic;

namespace Bam.Net.Presentation.Html
{
    public static class Tags
    {
        static Func<string, object, string, Tag> _tagProvider;

        public static Func<string, object, string, Tag> TagProvider
        {
            get { return _tagProvider ?? ((tagName, attributes, content) => new Tag(tagName, attributes, content)); }
            set { _tagProvider = value; }
        }

        /// <summary>
        /// Defines a hyperlink    
        /// </summary>
        public static Tag A(Func<Tag> content)
        {
            return A(new object(), content);
        }

        /// <summary>
        /// Defines a hyperlink
        /// </summary>
        public static Tag A(object attributes, Func<Tag> content)
        {
            return A(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a hyperlink
        /// </summary>            
        public static Tag A(Tag content)
        {
            return A(new object(), content.Render());
        }

        /// <summary>
        /// Defines a hyperlink
        /// </summary>
        public static Tag A(object attributes = null, string content = null)
        {
            return TagProvider("a", attributes, content);
        }

        /// <summary>
        /// Defines an abbreviation or an acronym    
        /// </summary>
        public static Tag Abbr(Func<Tag> content)
        {
            return Abbr(new object(), content);
        }

        /// <summary>
        /// Defines an abbreviation or an acronym
        /// </summary>
        public static Tag Abbr(object attributes, Func<Tag> content)
        {
            return Abbr(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an abbreviation or an acronym
        /// </summary>            
        public static Tag Abbr(Tag content)
        {
            return Abbr(new object(), content.Render());
        }

        /// <summary>
        /// Defines an abbreviation or an acronym
        /// </summary>
        public static Tag Abbr(object attributes = null, string content = null)
        {
            return TagProvider("abbr", attributes, content);
        }

        /// <summary>
        /// Defines an acronym    
        /// </summary>
        public static Tag Acronym(Func<Tag> content)
        {
            return Acronym(new object(), content);
        }

        /// <summary>
        /// Defines an acronym
        /// </summary>
        public static Tag Acronym(object attributes, Func<Tag> content)
        {
            return Acronym(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an acronym
        /// </summary>            
        public static Tag Acronym(Tag content)
        {
            return Acronym(new object(), content.Render());
        }

        /// <summary>
        /// Defines an acronym
        /// </summary>
        public static Tag Acronym(object attributes = null, string content = null)
        {
            return TagProvider("acronym", attributes, content);
        }

        /// <summary>
        /// Defines contact information for the author/owner of a document    
        /// </summary>
        public static Tag Address(Func<Tag> content)
        {
            return Address(new object(), content);
        }

        /// <summary>
        /// Defines contact information for the author/owner of a document
        /// </summary>
        public static Tag Address(object attributes, Func<Tag> content)
        {
            return Address(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines contact information for the author/owner of a document
        /// </summary>            
        public static Tag Address(Tag content)
        {
            return Address(new object(), content.Render());
        }

        /// <summary>
        /// Defines contact information for the author/owner of a document
        /// </summary>
        public static Tag Address(object attributes = null, string content = null)
        {
            return TagProvider("address", attributes, content);
        }

        /// <summary>
        /// Defines an embedded applet    
        /// </summary>
        public static Tag Applet(Func<Tag> content)
        {
            return Applet(new object(), content);
        }

        /// <summary>
        /// Defines an embedded applet
        /// </summary>
        public static Tag Applet(object attributes, Func<Tag> content)
        {
            return Applet(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an embedded applet
        /// </summary>            
        public static Tag Applet(Tag content)
        {
            return Applet(new object(), content.Render());
        }

        /// <summary>
        /// Defines an embedded applet
        /// </summary>
        public static Tag Applet(object attributes = null, string content = null)
        {
            return TagProvider("applet", attributes, content);
        }

        /// <summary>
        /// Defines an area inside an image-map    
        /// </summary>
        public static Tag Area(Func<Tag> content)
        {
            return Area(new object(), content);
        }

        /// <summary>
        /// Defines an area inside an image-map
        /// </summary>
        public static Tag Area(object attributes, Func<Tag> content)
        {
            return Area(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an area inside an image-map
        /// </summary>            
        public static Tag Area(Tag content)
        {
            return Area(new object(), content.Render());
        }

        /// <summary>
        /// Defines an area inside an image-map
        /// </summary>
        public static Tag Area(object attributes = null, string content = null)
        {
            return TagProvider("area", attributes, content);
        }

        /// <summary>
        /// Defines an article    
        /// </summary>
        public static Tag Article(Func<Tag> content)
        {
            return Article(new object(), content);
        }

        /// <summary>
        /// Defines an article
        /// </summary>
        public static Tag Article(object attributes, Func<Tag> content)
        {
            return Article(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an article
        /// </summary>            
        public static Tag Article(Tag content)
        {
            return Article(new object(), content.Render());
        }

        /// <summary>
        /// Defines an article
        /// </summary>
        public static Tag Article(object attributes = null, string content = null)
        {
            return TagProvider("article", attributes, content);
        }

        /// <summary>
        /// Defines content aside from the page content    
        /// </summary>
        public static Tag Aside(Func<Tag> content)
        {
            return Aside(new object(), content);
        }

        /// <summary>
        /// Defines content aside from the page content
        /// </summary>
        public static Tag Aside(object attributes, Func<Tag> content)
        {
            return Aside(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines content aside from the page content
        /// </summary>            
        public static Tag Aside(Tag content)
        {
            return Aside(new object(), content.Render());
        }

        /// <summary>
        /// Defines content aside from the page content
        /// </summary>
        public static Tag Aside(object attributes = null, string content = null)
        {
            return TagProvider("aside", attributes, content);
        }

        /// <summary>
        /// Defines sound content    
        /// </summary>
        public static Tag Audio(Func<Tag> content)
        {
            return Audio(new object(), content);
        }

        /// <summary>
        /// Defines sound content
        /// </summary>
        public static Tag Audio(object attributes, Func<Tag> content)
        {
            return Audio(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines sound content
        /// </summary>            
        public static Tag Audio(Tag content)
        {
            return Audio(new object(), content.Render());
        }

        /// <summary>
        /// Defines sound content
        /// </summary>
        public static Tag Audio(object attributes = null, string content = null)
        {
            return TagProvider("audio", attributes, content);
        }

        /// <summary>
        /// Defines bold text    
        /// </summary>
        public static Tag B(Func<Tag> content)
        {
            return B(new object(), content);
        }

        /// <summary>
        /// Defines bold text
        /// </summary>
        public static Tag B(object attributes, Func<Tag> content)
        {
            return B(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines bold text
        /// </summary>            
        public static Tag B(Tag content)
        {
            return B(new object(), content.Render());
        }

        /// <summary>
        /// Defines bold text
        /// </summary>
        public static Tag B(object attributes = null, string content = null)
        {
            return TagProvider("b", attributes, content);
        }

        /// <summary>
        /// Specifies the base URL/target for all relative URLs in a document    
        /// </summary>
        public static Tag Base(Func<Tag> content)
        {
            return Base(new object(), content);
        }

        /// <summary>
        /// Specifies the base URL/target for all relative URLs in a document
        /// </summary>
        public static Tag Base(object attributes, Func<Tag> content)
        {
            return Base(attributes, content()?.Render());
        }

        /// <summary>
        /// Specifies the base URL/target for all relative URLs in a document
        /// </summary>            
        public static Tag Base(Tag content)
        {
            return Base(new object(), content.Render());
        }

        /// <summary>
        /// Specifies the base URL/target for all relative URLs in a document
        /// </summary>
        public static Tag Base(object attributes = null, string content = null)
        {
            return TagProvider("base", attributes, content);
        }

        /// <summary>
        /// Specifies a default color, size, and font for all text in a document    
        /// </summary>
        public static Tag Basefont(Func<Tag> content)
        {
            return Basefont(new object(), content);
        }

        /// <summary>
        /// Specifies a default color, size, and font for all text in a document
        /// </summary>
        public static Tag Basefont(object attributes, Func<Tag> content)
        {
            return Basefont(attributes, content()?.Render());
        }

        /// <summary>
        /// Specifies a default color, size, and font for all text in a document
        /// </summary>            
        public static Tag Basefont(Tag content)
        {
            return Basefont(new object(), content.Render());
        }

        /// <summary>
        /// Specifies a default color, size, and font for all text in a document
        /// </summary>
        public static Tag Basefont(object attributes = null, string content = null)
        {
            return TagProvider("basefont", attributes, content);
        }

        /// <summary>
        /// Isolates a part of text that might be formatted in a different direction from other text outside it    
        /// </summary>
        public static Tag Bdi(Func<Tag> content)
        {
            return Bdi(new object(), content);
        }

        /// <summary>
        /// Isolates a part of text that might be formatted in a different direction from other text outside it
        /// </summary>
        public static Tag Bdi(object attributes, Func<Tag> content)
        {
            return Bdi(attributes, content()?.Render());
        }

        /// <summary>
        /// Isolates a part of text that might be formatted in a different direction from other text outside it
        /// </summary>            
        public static Tag Bdi(Tag content)
        {
            return Bdi(new object(), content.Render());
        }

        /// <summary>
        /// Isolates a part of text that might be formatted in a different direction from other text outside it
        /// </summary>
        public static Tag Bdi(object attributes = null, string content = null)
        {
            return TagProvider("bdi", attributes, content);
        }

        /// <summary>
        /// Overrides the current text direction    
        /// </summary>
        public static Tag Bdo(Func<Tag> content)
        {
            return Bdo(new object(), content);
        }

        /// <summary>
        /// Overrides the current text direction
        /// </summary>
        public static Tag Bdo(object attributes, Func<Tag> content)
        {
            return Bdo(attributes, content()?.Render());
        }

        /// <summary>
        /// Overrides the current text direction
        /// </summary>            
        public static Tag Bdo(Tag content)
        {
            return Bdo(new object(), content.Render());
        }

        /// <summary>
        /// Overrides the current text direction
        /// </summary>
        public static Tag Bdo(object attributes = null, string content = null)
        {
            return TagProvider("bdo", attributes, content);
        }

        /// <summary>
        /// Defines big text    
        /// </summary>
        public static Tag Big(Func<Tag> content)
        {
            return Big(new object(), content);
        }

        /// <summary>
        /// Defines big text
        /// </summary>
        public static Tag Big(object attributes, Func<Tag> content)
        {
            return Big(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines big text
        /// </summary>            
        public static Tag Big(Tag content)
        {
            return Big(new object(), content.Render());
        }

        /// <summary>
        /// Defines big text
        /// </summary>
        public static Tag Big(object attributes = null, string content = null)
        {
            return TagProvider("big", attributes, content);
        }

        /// <summary>
        /// Defines a section that is quoted from another source    
        /// </summary>
        public static Tag Blockquote(Func<Tag> content)
        {
            return Blockquote(new object(), content);
        }

        /// <summary>
        /// Defines a section that is quoted from another source
        /// </summary>
        public static Tag Blockquote(object attributes, Func<Tag> content)
        {
            return Blockquote(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a section that is quoted from another source
        /// </summary>            
        public static Tag Blockquote(Tag content)
        {
            return Blockquote(new object(), content.Render());
        }

        /// <summary>
        /// Defines a section that is quoted from another source
        /// </summary>
        public static Tag Blockquote(object attributes = null, string content = null)
        {
            return TagProvider("blockquote", attributes, content);
        }

        /// <summary>
        /// Defines the document's body    
        /// </summary>
        public static Tag Body(Func<Tag> content)
        {
            return Body(new object(), content);
        }

        /// <summary>
        /// Defines the document's body
        /// </summary>
        public static Tag Body(object attributes, Func<Tag> content)
        {
            return Body(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines the document's body
        /// </summary>            
        public static Tag Body(Tag content)
        {
            return Body(new object(), content.Render());
        }

        /// <summary>
        /// Defines the document's body
        /// </summary>
        public static Tag Body(object attributes = null, string content = null)
        {
            return TagProvider("body", attributes, content);
        }

        /// <summary>
        /// Defines a single line break    
        /// </summary>
        public static Tag Br(Func<Tag> content)
        {
            return Br(new object(), content);
        }

        /// <summary>
        /// Defines a single line break
        /// </summary>
        public static Tag Br(object attributes, Func<Tag> content)
        {
            return Br(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a single line break
        /// </summary>            
        public static Tag Br(Tag content)
        {
            return Br(new object(), content.Render());
        }

        /// <summary>
        /// Defines a single line break
        /// </summary>
        public static Tag Br(object attributes = null, string content = null)
        {
            return TagProvider("br", attributes, content);
        }

        /// <summary>
        /// Defines a clickable button    
        /// </summary>
        public static Tag Button(Func<Tag> content)
        {
            return Button(new object(), content);
        }

        /// <summary>
        /// Defines a clickable button
        /// </summary>
        public static Tag Button(object attributes, Func<Tag> content)
        {
            return Button(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a clickable button
        /// </summary>            
        public static Tag Button(Tag content)
        {
            return Button(new object(), content.Render());
        }

        /// <summary>
        /// Defines a clickable button
        /// </summary>
        public static Tag Button(object attributes = null, string content = null)
        {
            return TagProvider("button", attributes, content);
        }

        /// <summary>
        /// Used to draw graphics, on the fly, via scripting (usually JavaScript)    
        /// </summary>
        public static Tag Canvas(Func<Tag> content)
        {
            return Canvas(new object(), content);
        }

        /// <summary>
        /// Used to draw graphics, on the fly, via scripting (usually JavaScript)
        /// </summary>
        public static Tag Canvas(object attributes, Func<Tag> content)
        {
            return Canvas(attributes, content()?.Render());
        }

        /// <summary>
        /// Used to draw graphics, on the fly, via scripting (usually JavaScript)
        /// </summary>            
        public static Tag Canvas(Tag content)
        {
            return Canvas(new object(), content.Render());
        }

        /// <summary>
        /// Used to draw graphics, on the fly, via scripting (usually JavaScript)
        /// </summary>
        public static Tag Canvas(object attributes = null, string content = null)
        {
            return TagProvider("canvas", attributes, content);
        }

        /// <summary>
        /// Defines a table caption    
        /// </summary>
        public static Tag Caption(Func<Tag> content)
        {
            return Caption(new object(), content);
        }

        /// <summary>
        /// Defines a table caption
        /// </summary>
        public static Tag Caption(object attributes, Func<Tag> content)
        {
            return Caption(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a table caption
        /// </summary>            
        public static Tag Caption(Tag content)
        {
            return Caption(new object(), content.Render());
        }

        /// <summary>
        /// Defines a table caption
        /// </summary>
        public static Tag Caption(object attributes = null, string content = null)
        {
            return TagProvider("caption", attributes, content);
        }

        /// <summary>
        /// Defines centered text    
        /// </summary>
        public static Tag Center(Func<Tag> content)
        {
            return Center(new object(), content);
        }

        /// <summary>
        /// Defines centered text
        /// </summary>
        public static Tag Center(object attributes, Func<Tag> content)
        {
            return Center(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines centered text
        /// </summary>            
        public static Tag Center(Tag content)
        {
            return Center(new object(), content.Render());
        }

        /// <summary>
        /// Defines centered text
        /// </summary>
        public static Tag Center(object attributes = null, string content = null)
        {
            return TagProvider("center", attributes, content);
        }

        /// <summary>
        /// Defines the title of a work    
        /// </summary>
        public static Tag Cite(Func<Tag> content)
        {
            return Cite(new object(), content);
        }

        /// <summary>
        /// Defines the title of a work
        /// </summary>
        public static Tag Cite(object attributes, Func<Tag> content)
        {
            return Cite(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines the title of a work
        /// </summary>            
        public static Tag Cite(Tag content)
        {
            return Cite(new object(), content.Render());
        }

        /// <summary>
        /// Defines the title of a work
        /// </summary>
        public static Tag Cite(object attributes = null, string content = null)
        {
            return TagProvider("cite", attributes, content);
        }

        /// <summary>
        /// Defines a piece of computer code    
        /// </summary>
        public static Tag Code(Func<Tag> content)
        {
            return Code(new object(), content);
        }

        /// <summary>
        /// Defines a piece of computer code
        /// </summary>
        public static Tag Code(object attributes, Func<Tag> content)
        {
            return Code(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a piece of computer code
        /// </summary>            
        public static Tag Code(Tag content)
        {
            return Code(new object(), content.Render());
        }

        /// <summary>
        /// Defines a piece of computer code
        /// </summary>
        public static Tag Code(object attributes = null, string content = null)
        {
            return TagProvider("code", attributes, content);
        }

        /// <summary>
        /// Specifies column properties for each column within a &lt;colgroup&gt; element    
        /// </summary>
        public static Tag Col(Func<Tag> content)
        {
            return Col(new object(), content);
        }

        /// <summary>
        /// Specifies column properties for each column within a &lt;colgroup&gt; element
        /// </summary>
        public static Tag Col(object attributes, Func<Tag> content)
        {
            return Col(attributes, content()?.Render());
        }

        /// <summary>
        /// Specifies column properties for each column within a &lt;colgroup&gt; element
        /// </summary>            
        public static Tag Col(Tag content)
        {
            return Col(new object(), content.Render());
        }

        /// <summary>
        /// Specifies column properties for each column within a &lt;colgroup&gt; element
        /// </summary>
        public static Tag Col(object attributes = null, string content = null)
        {
            return TagProvider("col", attributes, content);
        }

        /// <summary>
        /// Specifies a group of one or more columns in a table for formatting    
        /// </summary>
        public static Tag Colgroup(Func<Tag> content)
        {
            return Colgroup(new object(), content);
        }

        /// <summary>
        /// Specifies a group of one or more columns in a table for formatting
        /// </summary>
        public static Tag Colgroup(object attributes, Func<Tag> content)
        {
            return Colgroup(attributes, content()?.Render());
        }

        /// <summary>
        /// Specifies a group of one or more columns in a table for formatting
        /// </summary>            
        public static Tag Colgroup(Tag content)
        {
            return Colgroup(new object(), content.Render());
        }

        /// <summary>
        /// Specifies a group of one or more columns in a table for formatting
        /// </summary>
        public static Tag Colgroup(object attributes = null, string content = null)
        {
            return TagProvider("colgroup", attributes, content);
        }

        /// <summary>
        /// Links the given content with a machine-readable translation    
        /// </summary>
        public static Tag Data(Func<Tag> content)
        {
            return Data(new object(), content);
        }

        /// <summary>
        /// Links the given content with a machine-readable translation
        /// </summary>
        public static Tag Data(object attributes, Func<Tag> content)
        {
            return Data(attributes, content()?.Render());
        }

        /// <summary>
        /// Links the given content with a machine-readable translation
        /// </summary>            
        public static Tag Data(Tag content)
        {
            return Data(new object(), content.Render());
        }

        /// <summary>
        /// Links the given content with a machine-readable translation
        /// </summary>
        public static Tag Data(object attributes = null, string content = null)
        {
            return TagProvider("data", attributes, content);
        }

        /// <summary>
        /// Specifies a list of pre-defined options for input controls    
        /// </summary>
        public static Tag Datalist(Func<Tag> content)
        {
            return Datalist(new object(), content);
        }

        /// <summary>
        /// Specifies a list of pre-defined options for input controls
        /// </summary>
        public static Tag Datalist(object attributes, Func<Tag> content)
        {
            return Datalist(attributes, content()?.Render());
        }

        /// <summary>
        /// Specifies a list of pre-defined options for input controls
        /// </summary>            
        public static Tag Datalist(Tag content)
        {
            return Datalist(new object(), content.Render());
        }

        /// <summary>
        /// Specifies a list of pre-defined options for input controls
        /// </summary>
        public static Tag Datalist(object attributes = null, string content = null)
        {
            return TagProvider("datalist", attributes, content);
        }

        /// <summary>
        /// Defines a description/value of a term in a description list    
        /// </summary>
        public static Tag Dd(Func<Tag> content)
        {
            return Dd(new object(), content);
        }

        /// <summary>
        /// Defines a description/value of a term in a description list
        /// </summary>
        public static Tag Dd(object attributes, Func<Tag> content)
        {
            return Dd(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a description/value of a term in a description list
        /// </summary>            
        public static Tag Dd(Tag content)
        {
            return Dd(new object(), content.Render());
        }

        /// <summary>
        /// Defines a description/value of a term in a description list
        /// </summary>
        public static Tag Dd(object attributes = null, string content = null)
        {
            return TagProvider("dd", attributes, content);
        }

        /// <summary>
        /// Defines text that has been deleted from a document    
        /// </summary>
        public static Tag Del(Func<Tag> content)
        {
            return Del(new object(), content);
        }

        /// <summary>
        /// Defines text that has been deleted from a document
        /// </summary>
        public static Tag Del(object attributes, Func<Tag> content)
        {
            return Del(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines text that has been deleted from a document
        /// </summary>            
        public static Tag Del(Tag content)
        {
            return Del(new object(), content.Render());
        }

        /// <summary>
        /// Defines text that has been deleted from a document
        /// </summary>
        public static Tag Del(object attributes = null, string content = null)
        {
            return TagProvider("del", attributes, content);
        }

        /// <summary>
        /// Defines additional details that the user can view or hide    
        /// </summary>
        public static Tag Details(Func<Tag> content)
        {
            return Details(new object(), content);
        }

        /// <summary>
        /// Defines additional details that the user can view or hide
        /// </summary>
        public static Tag Details(object attributes, Func<Tag> content)
        {
            return Details(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines additional details that the user can view or hide
        /// </summary>            
        public static Tag Details(Tag content)
        {
            return Details(new object(), content.Render());
        }

        /// <summary>
        /// Defines additional details that the user can view or hide
        /// </summary>
        public static Tag Details(object attributes = null, string content = null)
        {
            return TagProvider("details", attributes, content);
        }

        /// <summary>
        /// Represents the defining instance of a term    
        /// </summary>
        public static Tag Dfn(Func<Tag> content)
        {
            return Dfn(new object(), content);
        }

        /// <summary>
        /// Represents the defining instance of a term
        /// </summary>
        public static Tag Dfn(object attributes, Func<Tag> content)
        {
            return Dfn(attributes, content()?.Render());
        }

        /// <summary>
        /// Represents the defining instance of a term
        /// </summary>            
        public static Tag Dfn(Tag content)
        {
            return Dfn(new object(), content.Render());
        }

        /// <summary>
        /// Represents the defining instance of a term
        /// </summary>
        public static Tag Dfn(object attributes = null, string content = null)
        {
            return TagProvider("dfn", attributes, content);
        }

        /// <summary>
        /// Defines a dialog box or window    
        /// </summary>
        public static Tag Dialog(Func<Tag> content)
        {
            return Dialog(new object(), content);
        }

        /// <summary>
        /// Defines a dialog box or window
        /// </summary>
        public static Tag Dialog(object attributes, Func<Tag> content)
        {
            return Dialog(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a dialog box or window
        /// </summary>            
        public static Tag Dialog(Tag content)
        {
            return Dialog(new object(), content.Render());
        }

        /// <summary>
        /// Defines a dialog box or window
        /// </summary>
        public static Tag Dialog(object attributes = null, string content = null)
        {
            return TagProvider("dialog", attributes, content);
        }

        /// <summary>
        /// Defines a directory list    
        /// </summary>
        public static Tag Dir(Func<Tag> content)
        {
            return Dir(new object(), content);
        }

        /// <summary>
        /// Defines a directory list
        /// </summary>
        public static Tag Dir(object attributes, Func<Tag> content)
        {
            return Dir(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a directory list
        /// </summary>            
        public static Tag Dir(Tag content)
        {
            return Dir(new object(), content.Render());
        }

        /// <summary>
        /// Defines a directory list
        /// </summary>
        public static Tag Dir(object attributes = null, string content = null)
        {
            return TagProvider("dir", attributes, content);
        }

        /// <summary>
        /// Defines a section in a document    
        /// </summary>
        public static Tag Div(Func<Tag> content)
        {
            return Div(new object(), content);
        }

        /// <summary>
        /// Defines a section in a document
        /// </summary>
        public static Tag Div(object attributes, Func<Tag> content)
        {
            return Div(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a section in a document
        /// </summary>            
        public static Tag Div(Tag content)
        {
            return Div(new object(), content.Render());
        }

        /// <summary>
        /// Defines a section in a document
        /// </summary>
        public static Tag Div(object attributes = null, string content = null)
        {
            return TagProvider("div", attributes, content);
        }

        /// <summary>
        /// Defines a description list    
        /// </summary>
        public static Tag Dl(Func<Tag> content)
        {
            return Dl(new object(), content);
        }

        /// <summary>
        /// Defines a description list
        /// </summary>
        public static Tag Dl(object attributes, Func<Tag> content)
        {
            return Dl(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a description list
        /// </summary>            
        public static Tag Dl(Tag content)
        {
            return Dl(new object(), content.Render());
        }

        /// <summary>
        /// Defines a description list
        /// </summary>
        public static Tag Dl(object attributes = null, string content = null)
        {
            return TagProvider("dl", attributes, content);
        }

        /// <summary>
        /// Defines a term/name in a description list    
        /// </summary>
        public static Tag Dt(Func<Tag> content)
        {
            return Dt(new object(), content);
        }

        /// <summary>
        /// Defines a term/name in a description list
        /// </summary>
        public static Tag Dt(object attributes, Func<Tag> content)
        {
            return Dt(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a term/name in a description list
        /// </summary>            
        public static Tag Dt(Tag content)
        {
            return Dt(new object(), content.Render());
        }

        /// <summary>
        /// Defines a term/name in a description list
        /// </summary>
        public static Tag Dt(object attributes = null, string content = null)
        {
            return TagProvider("dt", attributes, content);
        }

        /// <summary>
        /// Defines emphasized text    
        /// </summary>
        public static Tag Em(Func<Tag> content)
        {
            return Em(new object(), content);
        }

        /// <summary>
        /// Defines emphasized text
        /// </summary>
        public static Tag Em(object attributes, Func<Tag> content)
        {
            return Em(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines emphasized text
        /// </summary>            
        public static Tag Em(Tag content)
        {
            return Em(new object(), content.Render());
        }

        /// <summary>
        /// Defines emphasized text
        /// </summary>
        public static Tag Em(object attributes = null, string content = null)
        {
            return TagProvider("em", attributes, content);
        }

        /// <summary>
        /// Defines a container for an external (non-HTML) application    
        /// </summary>
        public static Tag Embed(Func<Tag> content)
        {
            return Embed(new object(), content);
        }

        /// <summary>
        /// Defines a container for an external (non-HTML) application
        /// </summary>
        public static Tag Embed(object attributes, Func<Tag> content)
        {
            return Embed(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a container for an external (non-HTML) application
        /// </summary>            
        public static Tag Embed(Tag content)
        {
            return Embed(new object(), content.Render());
        }

        /// <summary>
        /// Defines a container for an external (non-HTML) application
        /// </summary>
        public static Tag Embed(object attributes = null, string content = null)
        {
            return TagProvider("embed", attributes, content);
        }

        /// <summary>
        /// Groups related elements in a form    
        /// </summary>
        public static Tag Fieldset(Func<Tag> content)
        {
            return Fieldset(new object(), content);
        }

        /// <summary>
        /// Groups related elements in a form
        /// </summary>
        public static Tag Fieldset(object attributes, Func<Tag> content)
        {
            return Fieldset(attributes, content()?.Render());
        }

        /// <summary>
        /// Groups related elements in a form
        /// </summary>            
        public static Tag Fieldset(Tag content)
        {
            return Fieldset(new object(), content.Render());
        }

        /// <summary>
        /// Groups related elements in a form
        /// </summary>
        public static Tag Fieldset(object attributes = null, string content = null)
        {
            return TagProvider("fieldset", attributes, content);
        }

        /// <summary>
        /// Defines a caption for a &lt;figure&gt; element    
        /// </summary>
        public static Tag Figcaption(Func<Tag> content)
        {
            return Figcaption(new object(), content);
        }

        /// <summary>
        /// Defines a caption for a &lt;figure&gt; element
        /// </summary>
        public static Tag Figcaption(object attributes, Func<Tag> content)
        {
            return Figcaption(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a caption for a &lt;figure&gt; element
        /// </summary>            
        public static Tag Figcaption(Tag content)
        {
            return Figcaption(new object(), content.Render());
        }

        /// <summary>
        /// Defines a caption for a &lt;figure&gt; element
        /// </summary>
        public static Tag Figcaption(object attributes = null, string content = null)
        {
            return TagProvider("figcaption", attributes, content);
        }

        /// <summary>
        /// Specifies self-contained content    
        /// </summary>
        public static Tag Figure(Func<Tag> content)
        {
            return Figure(new object(), content);
        }

        /// <summary>
        /// Specifies self-contained content
        /// </summary>
        public static Tag Figure(object attributes, Func<Tag> content)
        {
            return Figure(attributes, content()?.Render());
        }

        /// <summary>
        /// Specifies self-contained content
        /// </summary>            
        public static Tag Figure(Tag content)
        {
            return Figure(new object(), content.Render());
        }

        /// <summary>
        /// Specifies self-contained content
        /// </summary>
        public static Tag Figure(object attributes = null, string content = null)
        {
            return TagProvider("figure", attributes, content);
        }

        /// <summary>
        /// Defines font, color, and size for text    
        /// </summary>
        public static Tag Font(Func<Tag> content)
        {
            return Font(new object(), content);
        }

        /// <summary>
        /// Defines font, color, and size for text
        /// </summary>
        public static Tag Font(object attributes, Func<Tag> content)
        {
            return Font(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines font, color, and size for text
        /// </summary>            
        public static Tag Font(Tag content)
        {
            return Font(new object(), content.Render());
        }

        /// <summary>
        /// Defines font, color, and size for text
        /// </summary>
        public static Tag Font(object attributes = null, string content = null)
        {
            return TagProvider("font", attributes, content);
        }

        /// <summary>
        /// Defines a footer for a document or section    
        /// </summary>
        public static Tag Footer(Func<Tag> content)
        {
            return Footer(new object(), content);
        }

        /// <summary>
        /// Defines a footer for a document or section
        /// </summary>
        public static Tag Footer(object attributes, Func<Tag> content)
        {
            return Footer(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a footer for a document or section
        /// </summary>            
        public static Tag Footer(Tag content)
        {
            return Footer(new object(), content.Render());
        }

        /// <summary>
        /// Defines a footer for a document or section
        /// </summary>
        public static Tag Footer(object attributes = null, string content = null)
        {
            return TagProvider("footer", attributes, content);
        }

        /// <summary>
        /// Defines an HTML form for user input    
        /// </summary>
        public static Tag Form(Func<Tag> content)
        {
            return Form(new object(), content);
        }

        /// <summary>
        /// Defines an HTML form for user input
        /// </summary>
        public static Tag Form(object attributes, Func<Tag> content)
        {
            return Form(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an HTML form for user input
        /// </summary>            
        public static Tag Form(Tag content)
        {
            return Form(new object(), content.Render());
        }

        /// <summary>
        /// Defines an HTML form for user input
        /// </summary>
        public static Tag Form(object attributes = null, string content = null)
        {
            return TagProvider("form", attributes, content);
        }

        /// <summary>
        /// Defines a window (a frame) in a frameset    
        /// </summary>
        public static Tag Frame(Func<Tag> content)
        {
            return Frame(new object(), content);
        }

        /// <summary>
        /// Defines a window (a frame) in a frameset
        /// </summary>
        public static Tag Frame(object attributes, Func<Tag> content)
        {
            return Frame(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a window (a frame) in a frameset
        /// </summary>            
        public static Tag Frame(Tag content)
        {
            return Frame(new object(), content.Render());
        }

        /// <summary>
        /// Defines a window (a frame) in a frameset
        /// </summary>
        public static Tag Frame(object attributes = null, string content = null)
        {
            return TagProvider("frame", attributes, content);
        }

        /// <summary>
        /// Defines a set of frames    
        /// </summary>
        public static Tag Frameset(Func<Tag> content)
        {
            return Frameset(new object(), content);
        }

        /// <summary>
        /// Defines a set of frames
        /// </summary>
        public static Tag Frameset(object attributes, Func<Tag> content)
        {
            return Frameset(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a set of frames
        /// </summary>            
        public static Tag Frameset(Tag content)
        {
            return Frameset(new object(), content.Render());
        }

        /// <summary>
        /// Defines a set of frames
        /// </summary>
        public static Tag Frameset(object attributes = null, string content = null)
        {
            return TagProvider("frameset", attributes, content);
        }

        /// <summary>
        /// to &lt;h6&gt;	Defines HTML headings    
        /// </summary>
        public static Tag H1(Func<Tag> content)
        {
            return H1(new object(), content);
        }

        /// <summary>
        /// to &lt;h6&gt;	Defines HTML headings
        /// </summary>
        public static Tag H1(object attributes, Func<Tag> content)
        {
            return H1(attributes, content()?.Render());
        }

        /// <summary>
        /// to &lt;h6&gt;	Defines HTML headings
        /// </summary>            
        public static Tag H1(Tag content)
        {
            return H1(new object(), content.Render());
        }

        /// <summary>
        /// to &lt;h6&gt;	Defines HTML headings
        /// </summary>
        public static Tag H1(object attributes = null, string content = null)
        {
            return TagProvider("h1", attributes, content);
        }

        /// <summary>
        /// Defines information about the document    
        /// </summary>
        public static Tag Head(Func<Tag> content)
        {
            return Head(new object(), content);
        }

        /// <summary>
        /// Defines information about the document
        /// </summary>
        public static Tag Head(object attributes, Func<Tag> content)
        {
            return Head(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines information about the document
        /// </summary>            
        public static Tag Head(Tag content)
        {
            return Head(new object(), content.Render());
        }

        /// <summary>
        /// Defines information about the document
        /// </summary>
        public static Tag Head(object attributes = null, string content = null)
        {
            return TagProvider("head", attributes, content);
        }

        /// <summary>
        /// Defines a header for a document or section    
        /// </summary>
        public static Tag Header(Func<Tag> content)
        {
            return Header(new object(), content);
        }

        /// <summary>
        /// Defines a header for a document or section
        /// </summary>
        public static Tag Header(object attributes, Func<Tag> content)
        {
            return Header(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a header for a document or section
        /// </summary>            
        public static Tag Header(Tag content)
        {
            return Header(new object(), content.Render());
        }

        /// <summary>
        /// Defines a header for a document or section
        /// </summary>
        public static Tag Header(object attributes = null, string content = null)
        {
            return TagProvider("header", attributes, content);
        }

        /// <summary>
        /// Defines a thematic change in the content    
        /// </summary>
        public static Tag Hr(Func<Tag> content)
        {
            return Hr(new object(), content);
        }

        /// <summary>
        /// Defines a thematic change in the content
        /// </summary>
        public static Tag Hr(object attributes, Func<Tag> content)
        {
            return Hr(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a thematic change in the content
        /// </summary>            
        public static Tag Hr(Tag content)
        {
            return Hr(new object(), content.Render());
        }

        /// <summary>
        /// Defines a thematic change in the content
        /// </summary>
        public static Tag Hr(object attributes = null, string content = null)
        {
            return TagProvider("hr", attributes, content);
        }

        /// <summary>
        /// Defines the root of an HTML document    
        /// </summary>
        public static Tag Html(Func<Tag> content)
        {
            return Html(new object(), content);
        }

        /// <summary>
        /// Defines the root of an HTML document
        /// </summary>
        public static Tag Html(object attributes, Func<Tag> content)
        {
            return Html(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines the root of an HTML document
        /// </summary>            
        public static Tag Html(Tag content)
        {
            return Html(new object(), content.Render());
        }

        /// <summary>
        /// Defines the root of an HTML document
        /// </summary>
        public static Tag Html(object attributes = null, string content = null)
        {
            return TagProvider("html", attributes, content);
        }

        /// <summary>
        /// Defines a part of text in an alternate voice or mood    
        /// </summary>
        public static Tag I(Func<Tag> content)
        {
            return I(new object(), content);
        }

        /// <summary>
        /// Defines a part of text in an alternate voice or mood
        /// </summary>
        public static Tag I(object attributes, Func<Tag> content)
        {
            return I(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a part of text in an alternate voice or mood
        /// </summary>            
        public static Tag I(Tag content)
        {
            return I(new object(), content.Render());
        }

        /// <summary>
        /// Defines a part of text in an alternate voice or mood
        /// </summary>
        public static Tag I(object attributes = null, string content = null)
        {
            return TagProvider("i", attributes, content);
        }

        /// <summary>
        /// Defines an inline frame    
        /// </summary>
        public static Tag Iframe(Func<Tag> content)
        {
            return Iframe(new object(), content);
        }

        /// <summary>
        /// Defines an inline frame
        /// </summary>
        public static Tag Iframe(object attributes, Func<Tag> content)
        {
            return Iframe(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an inline frame
        /// </summary>            
        public static Tag Iframe(Tag content)
        {
            return Iframe(new object(), content.Render());
        }

        /// <summary>
        /// Defines an inline frame
        /// </summary>
        public static Tag Iframe(object attributes = null, string content = null)
        {
            return TagProvider("iframe", attributes, content);
        }

        /// <summary>
        /// Defines an image    
        /// </summary>
        public static Tag Img(Func<Tag> content)
        {
            return Img(new object(), content);
        }

        /// <summary>
        /// Defines an image
        /// </summary>
        public static Tag Img(object attributes, Func<Tag> content)
        {
            return Img(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an image
        /// </summary>            
        public static Tag Img(Tag content)
        {
            return Img(new object(), content.Render());
        }

        /// <summary>
        /// Defines an image
        /// </summary>
        public static Tag Img(object attributes = null, string content = null)
        {
            return TagProvider("img", attributes, content);
        }

        /// <summary>
        /// Defines an input control    
        /// </summary>
        public static Tag Input(Func<Tag> content)
        {
            return Input(new object(), content);
        }

        /// <summary>
        /// Defines an input control
        /// </summary>
        public static Tag Input(object attributes, Func<Tag> content)
        {
            return Input(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an input control
        /// </summary>            
        public static Tag Input(Tag content)
        {
            return Input(new object(), content.Render());
        }

        /// <summary>
        /// Defines an input control
        /// </summary>
        public static Tag Input(object attributes = null, string content = null)
        {
            return TagProvider("input", attributes, content);
        }

        /// <summary>
        /// Defines a text that has been inserted into a document    
        /// </summary>
        public static Tag Ins(Func<Tag> content)
        {
            return Ins(new object(), content);
        }

        /// <summary>
        /// Defines a text that has been inserted into a document
        /// </summary>
        public static Tag Ins(object attributes, Func<Tag> content)
        {
            return Ins(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a text that has been inserted into a document
        /// </summary>            
        public static Tag Ins(Tag content)
        {
            return Ins(new object(), content.Render());
        }

        /// <summary>
        /// Defines a text that has been inserted into a document
        /// </summary>
        public static Tag Ins(object attributes = null, string content = null)
        {
            return TagProvider("ins", attributes, content);
        }

        /// <summary>
        /// Defines keyboard input    
        /// </summary>
        public static Tag Kbd(Func<Tag> content)
        {
            return Kbd(new object(), content);
        }

        /// <summary>
        /// Defines keyboard input
        /// </summary>
        public static Tag Kbd(object attributes, Func<Tag> content)
        {
            return Kbd(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines keyboard input
        /// </summary>            
        public static Tag Kbd(Tag content)
        {
            return Kbd(new object(), content.Render());
        }

        /// <summary>
        /// Defines keyboard input
        /// </summary>
        public static Tag Kbd(object attributes = null, string content = null)
        {
            return TagProvider("kbd", attributes, content);
        }

        /// <summary>
        /// Defines a label for an &lt;input&gt; element    
        /// </summary>
        public static Tag Label(Func<Tag> content)
        {
            return Label(new object(), content);
        }

        /// <summary>
        /// Defines a label for an &lt;input&gt; element
        /// </summary>
        public static Tag Label(object attributes, Func<Tag> content)
        {
            return Label(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a label for an &lt;input&gt; element
        /// </summary>            
        public static Tag Label(Tag content)
        {
            return Label(new object(), content.Render());
        }

        /// <summary>
        /// Defines a label for an &lt;input&gt; element
        /// </summary>
        public static Tag Label(object attributes = null, string content = null)
        {
            return TagProvider("label", attributes, content);
        }

        /// <summary>
        /// Defines a caption for a &lt;fieldset&gt; element    
        /// </summary>
        public static Tag Legend(Func<Tag> content)
        {
            return Legend(new object(), content);
        }

        /// <summary>
        /// Defines a caption for a &lt;fieldset&gt; element
        /// </summary>
        public static Tag Legend(object attributes, Func<Tag> content)
        {
            return Legend(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a caption for a &lt;fieldset&gt; element
        /// </summary>            
        public static Tag Legend(Tag content)
        {
            return Legend(new object(), content.Render());
        }

        /// <summary>
        /// Defines a caption for a &lt;fieldset&gt; element
        /// </summary>
        public static Tag Legend(object attributes = null, string content = null)
        {
            return TagProvider("legend", attributes, content);
        }

        /// <summary>
        /// Defines a list item    
        /// </summary>
        public static Tag Li(Func<Tag> content)
        {
            return Li(new object(), content);
        }

        /// <summary>
        /// Defines a list item
        /// </summary>
        public static Tag Li(object attributes, Func<Tag> content)
        {
            return Li(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a list item
        /// </summary>            
        public static Tag Li(Tag content)
        {
            return Li(new object(), content.Render());
        }

        /// <summary>
        /// Defines a list item
        /// </summary>
        public static Tag Li(object attributes = null, string content = null)
        {
            return TagProvider("li", attributes, content);
        }

        /// <summary>
        /// Defines the relationship between a document and an external resource (most used to link to style sheets)    
        /// </summary>
        public static Tag Link(Func<Tag> content)
        {
            return Link(new object(), content);
        }

        /// <summary>
        /// Defines the relationship between a document and an external resource (most used to link to style sheets)
        /// </summary>
        public static Tag Link(object attributes, Func<Tag> content)
        {
            return Link(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines the relationship between a document and an external resource (most used to link to style sheets)
        /// </summary>            
        public static Tag Link(Tag content)
        {
            return Link(new object(), content.Render());
        }

        /// <summary>
        /// Defines the relationship between a document and an external resource (most used to link to style sheets)
        /// </summary>
        public static Tag Link(object attributes = null, string content = null)
        {
            return TagProvider("link", attributes, content);
        }

        /// <summary>
        /// Specifies the main content of a document    
        /// </summary>
        public static Tag Main(Func<Tag> content)
        {
            return Main(new object(), content);
        }

        /// <summary>
        /// Specifies the main content of a document
        /// </summary>
        public static Tag Main(object attributes, Func<Tag> content)
        {
            return Main(attributes, content()?.Render());
        }

        /// <summary>
        /// Specifies the main content of a document
        /// </summary>            
        public static Tag Main(Tag content)
        {
            return Main(new object(), content.Render());
        }

        /// <summary>
        /// Specifies the main content of a document
        /// </summary>
        public static Tag Main(object attributes = null, string content = null)
        {
            return TagProvider("main", attributes, content);
        }

        /// <summary>
        /// Defines a client-side image-map    
        /// </summary>
        public static Tag Map(Func<Tag> content)
        {
            return Map(new object(), content);
        }

        /// <summary>
        /// Defines a client-side image-map
        /// </summary>
        public static Tag Map(object attributes, Func<Tag> content)
        {
            return Map(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a client-side image-map
        /// </summary>            
        public static Tag Map(Tag content)
        {
            return Map(new object(), content.Render());
        }

        /// <summary>
        /// Defines a client-side image-map
        /// </summary>
        public static Tag Map(object attributes = null, string content = null)
        {
            return TagProvider("map", attributes, content);
        }

        /// <summary>
        /// Defines marked/highlighted text    
        /// </summary>
        public static Tag Mark(Func<Tag> content)
        {
            return Mark(new object(), content);
        }

        /// <summary>
        /// Defines marked/highlighted text
        /// </summary>
        public static Tag Mark(object attributes, Func<Tag> content)
        {
            return Mark(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines marked/highlighted text
        /// </summary>            
        public static Tag Mark(Tag content)
        {
            return Mark(new object(), content.Render());
        }

        /// <summary>
        /// Defines marked/highlighted text
        /// </summary>
        public static Tag Mark(object attributes = null, string content = null)
        {
            return TagProvider("mark", attributes, content);
        }

        /// <summary>
        /// Defines metadata about an HTML document    
        /// </summary>
        public static Tag Meta(Func<Tag> content)
        {
            return Meta(new object(), content);
        }

        /// <summary>
        /// Defines metadata about an HTML document
        /// </summary>
        public static Tag Meta(object attributes, Func<Tag> content)
        {
            return Meta(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines metadata about an HTML document
        /// </summary>            
        public static Tag Meta(Tag content)
        {
            return Meta(new object(), content.Render());
        }

        /// <summary>
        /// Defines metadata about an HTML document
        /// </summary>
        public static Tag Meta(object attributes = null, string content = null)
        {
            return TagProvider("meta", attributes, content);
        }

        /// <summary>
        /// Defines a scalar measurement within a known range (a gauge)    
        /// </summary>
        public static Tag Meter(Func<Tag> content)
        {
            return Meter(new object(), content);
        }

        /// <summary>
        /// Defines a scalar measurement within a known range (a gauge)
        /// </summary>
        public static Tag Meter(object attributes, Func<Tag> content)
        {
            return Meter(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a scalar measurement within a known range (a gauge)
        /// </summary>            
        public static Tag Meter(Tag content)
        {
            return Meter(new object(), content.Render());
        }

        /// <summary>
        /// Defines a scalar measurement within a known range (a gauge)
        /// </summary>
        public static Tag Meter(object attributes = null, string content = null)
        {
            return TagProvider("meter", attributes, content);
        }

        /// <summary>
        /// Defines navigation links    
        /// </summary>
        public static Tag Nav(Func<Tag> content)
        {
            return Nav(new object(), content);
        }

        /// <summary>
        /// Defines navigation links
        /// </summary>
        public static Tag Nav(object attributes, Func<Tag> content)
        {
            return Nav(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines navigation links
        /// </summary>            
        public static Tag Nav(Tag content)
        {
            return Nav(new object(), content.Render());
        }

        /// <summary>
        /// Defines navigation links
        /// </summary>
        public static Tag Nav(object attributes = null, string content = null)
        {
            return TagProvider("nav", attributes, content);
        }

        /// <summary>
        /// Defines an alternate content for users that do not support frames    
        /// </summary>
        public static Tag Noframes(Func<Tag> content)
        {
            return Noframes(new object(), content);
        }

        /// <summary>
        /// Defines an alternate content for users that do not support frames
        /// </summary>
        public static Tag Noframes(object attributes, Func<Tag> content)
        {
            return Noframes(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an alternate content for users that do not support frames
        /// </summary>            
        public static Tag Noframes(Tag content)
        {
            return Noframes(new object(), content.Render());
        }

        /// <summary>
        /// Defines an alternate content for users that do not support frames
        /// </summary>
        public static Tag Noframes(object attributes = null, string content = null)
        {
            return TagProvider("noframes", attributes, content);
        }

        /// <summary>
        /// Defines an alternate content for users that do not support client-side scripts    
        /// </summary>
        public static Tag Noscript(Func<Tag> content)
        {
            return Noscript(new object(), content);
        }

        /// <summary>
        /// Defines an alternate content for users that do not support client-side scripts
        /// </summary>
        public static Tag Noscript(object attributes, Func<Tag> content)
        {
            return Noscript(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an alternate content for users that do not support client-side scripts
        /// </summary>            
        public static Tag Noscript(Tag content)
        {
            return Noscript(new object(), content.Render());
        }

        /// <summary>
        /// Defines an alternate content for users that do not support client-side scripts
        /// </summary>
        public static Tag Noscript(object attributes = null, string content = null)
        {
            return TagProvider("noscript", attributes, content);
        }

        /// <summary>
        /// Defines an embedded object    
        /// </summary>
        public static Tag Object(Func<Tag> content)
        {
            return Object(new object(), content);
        }

        /// <summary>
        /// Defines an embedded object
        /// </summary>
        public static Tag Object(object attributes, Func<Tag> content)
        {
            return Object(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an embedded object
        /// </summary>            
        public static Tag Object(Tag content)
        {
            return Object(new object(), content.Render());
        }

        /// <summary>
        /// Defines an embedded object
        /// </summary>
        public static Tag Object(object attributes = null, string content = null)
        {
            return TagProvider("object", attributes, content);
        }

        /// <summary>
        /// Defines an ordered list    
        /// </summary>
        public static Tag Ol(Func<Tag> content)
        {
            return Ol(new object(), content);
        }

        /// <summary>
        /// Defines an ordered list
        /// </summary>
        public static Tag Ol(object attributes, Func<Tag> content)
        {
            return Ol(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an ordered list
        /// </summary>            
        public static Tag Ol(Tag content)
        {
            return Ol(new object(), content.Render());
        }

        /// <summary>
        /// Defines an ordered list
        /// </summary>
        public static Tag Ol(object attributes = null, string content = null)
        {
            return TagProvider("ol", attributes, content);
        }

        /// <summary>
        /// Defines a group of related options in a drop-down list    
        /// </summary>
        public static Tag Optgroup(Func<Tag> content)
        {
            return Optgroup(new object(), content);
        }

        /// <summary>
        /// Defines a group of related options in a drop-down list
        /// </summary>
        public static Tag Optgroup(object attributes, Func<Tag> content)
        {
            return Optgroup(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a group of related options in a drop-down list
        /// </summary>            
        public static Tag Optgroup(Tag content)
        {
            return Optgroup(new object(), content.Render());
        }

        /// <summary>
        /// Defines a group of related options in a drop-down list
        /// </summary>
        public static Tag Optgroup(object attributes = null, string content = null)
        {
            return TagProvider("optgroup", attributes, content);
        }

        /// <summary>
        /// Defines an option in a drop-down list    
        /// </summary>
        public static Tag Option(Func<Tag> content)
        {
            return Option(new object(), content);
        }

        /// <summary>
        /// Defines an option in a drop-down list
        /// </summary>
        public static Tag Option(object attributes, Func<Tag> content)
        {
            return Option(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an option in a drop-down list
        /// </summary>            
        public static Tag Option(Tag content)
        {
            return Option(new object(), content.Render());
        }

        /// <summary>
        /// Defines an option in a drop-down list
        /// </summary>
        public static Tag Option(object attributes = null, string content = null)
        {
            return TagProvider("option", attributes, content);
        }

        /// <summary>
        /// Defines the result of a calculation    
        /// </summary>
        public static Tag Output(Func<Tag> content)
        {
            return Output(new object(), content);
        }

        /// <summary>
        /// Defines the result of a calculation
        /// </summary>
        public static Tag Output(object attributes, Func<Tag> content)
        {
            return Output(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines the result of a calculation
        /// </summary>            
        public static Tag Output(Tag content)
        {
            return Output(new object(), content.Render());
        }

        /// <summary>
        /// Defines the result of a calculation
        /// </summary>
        public static Tag Output(object attributes = null, string content = null)
        {
            return TagProvider("output", attributes, content);
        }

        /// <summary>
        /// Defines a paragraph    
        /// </summary>
        public static Tag P(Func<Tag> content)
        {
            return P(new object(), content);
        }

        /// <summary>
        /// Defines a paragraph
        /// </summary>
        public static Tag P(object attributes, Func<Tag> content)
        {
            return P(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a paragraph
        /// </summary>            
        public static Tag P(Tag content)
        {
            return P(new object(), content.Render());
        }

        /// <summary>
        /// Defines a paragraph
        /// </summary>
        public static Tag P(object attributes = null, string content = null)
        {
            return TagProvider("p", attributes, content);
        }

        /// <summary>
        /// Defines a parameter for an object    
        /// </summary>
        public static Tag Param(Func<Tag> content)
        {
            return Param(new object(), content);
        }

        /// <summary>
        /// Defines a parameter for an object
        /// </summary>
        public static Tag Param(object attributes, Func<Tag> content)
        {
            return Param(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a parameter for an object
        /// </summary>            
        public static Tag Param(Tag content)
        {
            return Param(new object(), content.Render());
        }

        /// <summary>
        /// Defines a parameter for an object
        /// </summary>
        public static Tag Param(object attributes = null, string content = null)
        {
            return TagProvider("param", attributes, content);
        }

        /// <summary>
        /// Defines a container for multiple image resources    
        /// </summary>
        public static Tag Picture(Func<Tag> content)
        {
            return Picture(new object(), content);
        }

        /// <summary>
        /// Defines a container for multiple image resources
        /// </summary>
        public static Tag Picture(object attributes, Func<Tag> content)
        {
            return Picture(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a container for multiple image resources
        /// </summary>            
        public static Tag Picture(Tag content)
        {
            return Picture(new object(), content.Render());
        }

        /// <summary>
        /// Defines a container for multiple image resources
        /// </summary>
        public static Tag Picture(object attributes = null, string content = null)
        {
            return TagProvider("picture", attributes, content);
        }

        /// <summary>
        /// Defines preformatted text    
        /// </summary>
        public static Tag Pre(Func<Tag> content)
        {
            return Pre(new object(), content);
        }

        /// <summary>
        /// Defines preformatted text
        /// </summary>
        public static Tag Pre(object attributes, Func<Tag> content)
        {
            return Pre(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines preformatted text
        /// </summary>            
        public static Tag Pre(Tag content)
        {
            return Pre(new object(), content.Render());
        }

        /// <summary>
        /// Defines preformatted text
        /// </summary>
        public static Tag Pre(object attributes = null, string content = null)
        {
            return TagProvider("pre", attributes, content);
        }

        /// <summary>
        /// Represents the progress of a task    
        /// </summary>
        public static Tag Progress(Func<Tag> content)
        {
            return Progress(new object(), content);
        }

        /// <summary>
        /// Represents the progress of a task
        /// </summary>
        public static Tag Progress(object attributes, Func<Tag> content)
        {
            return Progress(attributes, content()?.Render());
        }

        /// <summary>
        /// Represents the progress of a task
        /// </summary>            
        public static Tag Progress(Tag content)
        {
            return Progress(new object(), content.Render());
        }

        /// <summary>
        /// Represents the progress of a task
        /// </summary>
        public static Tag Progress(object attributes = null, string content = null)
        {
            return TagProvider("progress", attributes, content);
        }

        /// <summary>
        /// Defines a short quotation    
        /// </summary>
        public static Tag Q(Func<Tag> content)
        {
            return Q(new object(), content);
        }

        /// <summary>
        /// Defines a short quotation
        /// </summary>
        public static Tag Q(object attributes, Func<Tag> content)
        {
            return Q(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a short quotation
        /// </summary>            
        public static Tag Q(Tag content)
        {
            return Q(new object(), content.Render());
        }

        /// <summary>
        /// Defines a short quotation
        /// </summary>
        public static Tag Q(object attributes = null, string content = null)
        {
            return TagProvider("q", attributes, content);
        }

        /// <summary>
        /// Defines what to show in browsers that do not support ruby annotations    
        /// </summary>
        public static Tag Rp(Func<Tag> content)
        {
            return Rp(new object(), content);
        }

        /// <summary>
        /// Defines what to show in browsers that do not support ruby annotations
        /// </summary>
        public static Tag Rp(object attributes, Func<Tag> content)
        {
            return Rp(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines what to show in browsers that do not support ruby annotations
        /// </summary>            
        public static Tag Rp(Tag content)
        {
            return Rp(new object(), content.Render());
        }

        /// <summary>
        /// Defines what to show in browsers that do not support ruby annotations
        /// </summary>
        public static Tag Rp(object attributes = null, string content = null)
        {
            return TagProvider("rp", attributes, content);
        }

        /// <summary>
        /// Defines an explanation/pronunciation of characters (for East Asian typography)    
        /// </summary>
        public static Tag Rt(Func<Tag> content)
        {
            return Rt(new object(), content);
        }

        /// <summary>
        /// Defines an explanation/pronunciation of characters (for East Asian typography)
        /// </summary>
        public static Tag Rt(object attributes, Func<Tag> content)
        {
            return Rt(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an explanation/pronunciation of characters (for East Asian typography)
        /// </summary>            
        public static Tag Rt(Tag content)
        {
            return Rt(new object(), content.Render());
        }

        /// <summary>
        /// Defines an explanation/pronunciation of characters (for East Asian typography)
        /// </summary>
        public static Tag Rt(object attributes = null, string content = null)
        {
            return TagProvider("rt", attributes, content);
        }

        /// <summary>
        /// Defines a ruby annotation (for East Asian typography)    
        /// </summary>
        public static Tag Ruby(Func<Tag> content)
        {
            return Ruby(new object(), content);
        }

        /// <summary>
        /// Defines a ruby annotation (for East Asian typography)
        /// </summary>
        public static Tag Ruby(object attributes, Func<Tag> content)
        {
            return Ruby(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a ruby annotation (for East Asian typography)
        /// </summary>            
        public static Tag Ruby(Tag content)
        {
            return Ruby(new object(), content.Render());
        }

        /// <summary>
        /// Defines a ruby annotation (for East Asian typography)
        /// </summary>
        public static Tag Ruby(object attributes = null, string content = null)
        {
            return TagProvider("ruby", attributes, content);
        }

        /// <summary>
        /// Defines text that is no longer correct    
        /// </summary>
        public static Tag S(Func<Tag> content)
        {
            return S(new object(), content);
        }

        /// <summary>
        /// Defines text that is no longer correct
        /// </summary>
        public static Tag S(object attributes, Func<Tag> content)
        {
            return S(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines text that is no longer correct
        /// </summary>            
        public static Tag S(Tag content)
        {
            return S(new object(), content.Render());
        }

        /// <summary>
        /// Defines text that is no longer correct
        /// </summary>
        public static Tag S(object attributes = null, string content = null)
        {
            return TagProvider("s", attributes, content);
        }

        /// <summary>
        /// Defines sample output from a computer program    
        /// </summary>
        public static Tag Samp(Func<Tag> content)
        {
            return Samp(new object(), content);
        }

        /// <summary>
        /// Defines sample output from a computer program
        /// </summary>
        public static Tag Samp(object attributes, Func<Tag> content)
        {
            return Samp(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines sample output from a computer program
        /// </summary>            
        public static Tag Samp(Tag content)
        {
            return Samp(new object(), content.Render());
        }

        /// <summary>
        /// Defines sample output from a computer program
        /// </summary>
        public static Tag Samp(object attributes = null, string content = null)
        {
            return TagProvider("samp", attributes, content);
        }

        /// <summary>
        /// Defines a client-side script    
        /// </summary>
        public static Tag Script(Func<Tag> content)
        {
            return Script(new object(), content);
        }

        /// <summary>
        /// Defines a client-side script
        /// </summary>
        public static Tag Script(object attributes, Func<Tag> content)
        {
            return Script(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a client-side script
        /// </summary>            
        public static Tag Script(Tag content)
        {
            return Script(new object(), content.Render());
        }

        /// <summary>
        /// Defines a client-side script
        /// </summary>
        public static Tag Script(object attributes = null, string content = null)
        {
            return TagProvider("script", attributes, content);
        }

        /// <summary>
        /// Defines a section in a document    
        /// </summary>
        public static Tag Section(Func<Tag> content)
        {
            return Section(new object(), content);
        }

        /// <summary>
        /// Defines a section in a document
        /// </summary>
        public static Tag Section(object attributes, Func<Tag> content)
        {
            return Section(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a section in a document
        /// </summary>            
        public static Tag Section(Tag content)
        {
            return Section(new object(), content.Render());
        }

        /// <summary>
        /// Defines a section in a document
        /// </summary>
        public static Tag Section(object attributes = null, string content = null)
        {
            return TagProvider("section", attributes, content);
        }

        /// <summary>
        /// Defines a drop-down list    
        /// </summary>
        public static Tag Select(Func<Tag> content)
        {
            return Select(new object(), content);
        }

        /// <summary>
        /// Defines a drop-down list
        /// </summary>
        public static Tag Select(object attributes, Func<Tag> content)
        {
            return Select(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a drop-down list
        /// </summary>            
        public static Tag Select(Tag content)
        {
            return Select(new object(), content.Render());
        }

        /// <summary>
        /// Defines a drop-down list
        /// </summary>
        public static Tag Select(object attributes = null, string content = null)
        {
            return TagProvider("select", attributes, content);
        }

        /// <summary>
        /// Defines smaller text    
        /// </summary>
        public static Tag Small(Func<Tag> content)
        {
            return Small(new object(), content);
        }

        /// <summary>
        /// Defines smaller text
        /// </summary>
        public static Tag Small(object attributes, Func<Tag> content)
        {
            return Small(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines smaller text
        /// </summary>            
        public static Tag Small(Tag content)
        {
            return Small(new object(), content.Render());
        }

        /// <summary>
        /// Defines smaller text
        /// </summary>
        public static Tag Small(object attributes = null, string content = null)
        {
            return TagProvider("small", attributes, content);
        }

        /// <summary>
        /// Defines multiple media resources for media elements (&lt;video&gt; and &lt;audio&gt;)    
        /// </summary>
        public static Tag Source(Func<Tag> content)
        {
            return Source(new object(), content);
        }

        /// <summary>
        /// Defines multiple media resources for media elements (&lt;video&gt; and &lt;audio&gt;)
        /// </summary>
        public static Tag Source(object attributes, Func<Tag> content)
        {
            return Source(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines multiple media resources for media elements (&lt;video&gt; and &lt;audio&gt;)
        /// </summary>            
        public static Tag Source(Tag content)
        {
            return Source(new object(), content.Render());
        }

        /// <summary>
        /// Defines multiple media resources for media elements (&lt;video&gt; and &lt;audio&gt;)
        /// </summary>
        public static Tag Source(object attributes = null, string content = null)
        {
            return TagProvider("source", attributes, content);
        }

        /// <summary>
        /// Defines a section in a document    
        /// </summary>
        public static Tag Span(Func<Tag> content)
        {
            return Span(new object(), content);
        }

        /// <summary>
        /// Defines a section in a document
        /// </summary>
        public static Tag Span(object attributes, Func<Tag> content)
        {
            return Span(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a section in a document
        /// </summary>            
        public static Tag Span(Tag content)
        {
            return Span(new object(), content.Render());
        }

        /// <summary>
        /// Defines a section in a document
        /// </summary>
        public static Tag Span(object attributes = null, string content = null)
        {
            return TagProvider("span", attributes, content);
        }

        /// <summary>
        /// Defines strikethrough text    
        /// </summary>
        public static Tag Strike(Func<Tag> content)
        {
            return Strike(new object(), content);
        }

        /// <summary>
        /// Defines strikethrough text
        /// </summary>
        public static Tag Strike(object attributes, Func<Tag> content)
        {
            return Strike(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines strikethrough text
        /// </summary>            
        public static Tag Strike(Tag content)
        {
            return Strike(new object(), content.Render());
        }

        /// <summary>
        /// Defines strikethrough text
        /// </summary>
        public static Tag Strike(object attributes = null, string content = null)
        {
            return TagProvider("strike", attributes, content);
        }

        /// <summary>
        /// Defines important text    
        /// </summary>
        public static Tag Strong(Func<Tag> content)
        {
            return Strong(new object(), content);
        }

        /// <summary>
        /// Defines important text
        /// </summary>
        public static Tag Strong(object attributes, Func<Tag> content)
        {
            return Strong(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines important text
        /// </summary>            
        public static Tag Strong(Tag content)
        {
            return Strong(new object(), content.Render());
        }

        /// <summary>
        /// Defines important text
        /// </summary>
        public static Tag Strong(object attributes = null, string content = null)
        {
            return TagProvider("strong", attributes, content);
        }

        /// <summary>
        /// Defines style information for a document    
        /// </summary>
        public static Tag Style(Func<Tag> content)
        {
            return Style(new object(), content);
        }

        /// <summary>
        /// Defines style information for a document
        /// </summary>
        public static Tag Style(object attributes, Func<Tag> content)
        {
            return Style(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines style information for a document
        /// </summary>            
        public static Tag Style(Tag content)
        {
            return Style(new object(), content.Render());
        }

        /// <summary>
        /// Defines style information for a document
        /// </summary>
        public static Tag Style(object attributes = null, string content = null)
        {
            return TagProvider("style", attributes, content);
        }

        /// <summary>
        /// Defines subscripted text    
        /// </summary>
        public static Tag Sub(Func<Tag> content)
        {
            return Sub(new object(), content);
        }

        /// <summary>
        /// Defines subscripted text
        /// </summary>
        public static Tag Sub(object attributes, Func<Tag> content)
        {
            return Sub(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines subscripted text
        /// </summary>            
        public static Tag Sub(Tag content)
        {
            return Sub(new object(), content.Render());
        }

        /// <summary>
        /// Defines subscripted text
        /// </summary>
        public static Tag Sub(object attributes = null, string content = null)
        {
            return TagProvider("sub", attributes, content);
        }

        /// <summary>
        /// Defines a visible heading for a &lt;details&gt; element    
        /// </summary>
        public static Tag Summary(Func<Tag> content)
        {
            return Summary(new object(), content);
        }

        /// <summary>
        /// Defines a visible heading for a &lt;details&gt; element
        /// </summary>
        public static Tag Summary(object attributes, Func<Tag> content)
        {
            return Summary(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a visible heading for a &lt;details&gt; element
        /// </summary>            
        public static Tag Summary(Tag content)
        {
            return Summary(new object(), content.Render());
        }

        /// <summary>
        /// Defines a visible heading for a &lt;details&gt; element
        /// </summary>
        public static Tag Summary(object attributes = null, string content = null)
        {
            return TagProvider("summary", attributes, content);
        }

        /// <summary>
        /// Defines superscripted text    
        /// </summary>
        public static Tag Sup(Func<Tag> content)
        {
            return Sup(new object(), content);
        }

        /// <summary>
        /// Defines superscripted text
        /// </summary>
        public static Tag Sup(object attributes, Func<Tag> content)
        {
            return Sup(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines superscripted text
        /// </summary>            
        public static Tag Sup(Tag content)
        {
            return Sup(new object(), content.Render());
        }

        /// <summary>
        /// Defines superscripted text
        /// </summary>
        public static Tag Sup(object attributes = null, string content = null)
        {
            return TagProvider("sup", attributes, content);
        }

        /// <summary>
        /// Defines a container for SVG graphics    
        /// </summary>
        public static Tag Svg(Func<Tag> content)
        {
            return Svg(new object(), content);
        }

        /// <summary>
        /// Defines a container for SVG graphics
        /// </summary>
        public static Tag Svg(object attributes, Func<Tag> content)
        {
            return Svg(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a container for SVG graphics
        /// </summary>            
        public static Tag Svg(Tag content)
        {
            return Svg(new object(), content.Render());
        }

        /// <summary>
        /// Defines a container for SVG graphics
        /// </summary>
        public static Tag Svg(object attributes = null, string content = null)
        {
            return TagProvider("svg", attributes, content);
        }

        /// <summary>
        /// Defines a table    
        /// </summary>
        public static Tag Table(Func<Tag> content)
        {
            return Table(new object(), content);
        }

        /// <summary>
        /// Defines a table
        /// </summary>
        public static Tag Table(object attributes, Func<Tag> content)
        {
            return Table(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a table
        /// </summary>            
        public static Tag Table(Tag content)
        {
            return Table(new object(), content.Render());
        }

        /// <summary>
        /// Defines a table
        /// </summary>
        public static Tag Table(object attributes = null, string content = null)
        {
            return TagProvider("table", attributes, content);
        }

        /// <summary>
        /// Groups the body content in a table    
        /// </summary>
        public static Tag Tbody(Func<Tag> content)
        {
            return Tbody(new object(), content);
        }

        /// <summary>
        /// Groups the body content in a table
        /// </summary>
        public static Tag Tbody(object attributes, Func<Tag> content)
        {
            return Tbody(attributes, content()?.Render());
        }

        /// <summary>
        /// Groups the body content in a table
        /// </summary>            
        public static Tag Tbody(Tag content)
        {
            return Tbody(new object(), content.Render());
        }

        /// <summary>
        /// Groups the body content in a table
        /// </summary>
        public static Tag Tbody(object attributes = null, string content = null)
        {
            return TagProvider("tbody", attributes, content);
        }

        /// <summary>
        /// Defines a cell in a table    
        /// </summary>
        public static Tag Td(Func<Tag> content)
        {
            return Td(new object(), content);
        }

        /// <summary>
        /// Defines a cell in a table
        /// </summary>
        public static Tag Td(object attributes, Func<Tag> content)
        {
            return Td(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a cell in a table
        /// </summary>            
        public static Tag Td(Tag content)
        {
            return Td(new object(), content.Render());
        }

        /// <summary>
        /// Defines a cell in a table
        /// </summary>
        public static Tag Td(object attributes = null, string content = null)
        {
            return TagProvider("td", attributes, content);
        }

        /// <summary>
        /// Defines a template    
        /// </summary>
        public static Tag Template(Func<Tag> content)
        {
            return Template(new object(), content);
        }

        /// <summary>
        /// Defines a template
        /// </summary>
        public static Tag Template(object attributes, Func<Tag> content)
        {
            return Template(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a template
        /// </summary>            
        public static Tag Template(Tag content)
        {
            return Template(new object(), content.Render());
        }

        /// <summary>
        /// Defines a template
        /// </summary>
        public static Tag Template(object attributes = null, string content = null)
        {
            return TagProvider("template", attributes, content);
        }

        /// <summary>
        /// Defines a multiline input control (text area)    
        /// </summary>
        public static Tag Textarea(Func<Tag> content)
        {
            return Textarea(new object(), content);
        }

        /// <summary>
        /// Defines a multiline input control (text area)
        /// </summary>
        public static Tag Textarea(object attributes, Func<Tag> content)
        {
            return Textarea(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a multiline input control (text area)
        /// </summary>            
        public static Tag Textarea(Tag content)
        {
            return Textarea(new object(), content.Render());
        }

        /// <summary>
        /// Defines a multiline input control (text area)
        /// </summary>
        public static Tag Textarea(object attributes = null, string content = null)
        {
            return TagProvider("textarea", attributes, content);
        }

        /// <summary>
        /// Groups the footer content in a table    
        /// </summary>
        public static Tag Tfoot(Func<Tag> content)
        {
            return Tfoot(new object(), content);
        }

        /// <summary>
        /// Groups the footer content in a table
        /// </summary>
        public static Tag Tfoot(object attributes, Func<Tag> content)
        {
            return Tfoot(attributes, content()?.Render());
        }

        /// <summary>
        /// Groups the footer content in a table
        /// </summary>            
        public static Tag Tfoot(Tag content)
        {
            return Tfoot(new object(), content.Render());
        }

        /// <summary>
        /// Groups the footer content in a table
        /// </summary>
        public static Tag Tfoot(object attributes = null, string content = null)
        {
            return TagProvider("tfoot", attributes, content);
        }

        /// <summary>
        /// Defines a header cell in a table    
        /// </summary>
        public static Tag Th(Func<Tag> content)
        {
            return Th(new object(), content);
        }

        /// <summary>
        /// Defines a header cell in a table
        /// </summary>
        public static Tag Th(object attributes, Func<Tag> content)
        {
            return Th(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a header cell in a table
        /// </summary>            
        public static Tag Th(Tag content)
        {
            return Th(new object(), content.Render());
        }

        /// <summary>
        /// Defines a header cell in a table
        /// </summary>
        public static Tag Th(object attributes = null, string content = null)
        {
            return TagProvider("th", attributes, content);
        }

        /// <summary>
        /// Groups the header content in a table    
        /// </summary>
        public static Tag Thead(Func<Tag> content)
        {
            return Thead(new object(), content);
        }

        /// <summary>
        /// Groups the header content in a table
        /// </summary>
        public static Tag Thead(object attributes, Func<Tag> content)
        {
            return Thead(attributes, content()?.Render());
        }

        /// <summary>
        /// Groups the header content in a table
        /// </summary>            
        public static Tag Thead(Tag content)
        {
            return Thead(new object(), content.Render());
        }

        /// <summary>
        /// Groups the header content in a table
        /// </summary>
        public static Tag Thead(object attributes = null, string content = null)
        {
            return TagProvider("thead", attributes, content);
        }

        /// <summary>
        /// Defines a date/time    
        /// </summary>
        public static Tag Time(Func<Tag> content)
        {
            return Time(new object(), content);
        }

        /// <summary>
        /// Defines a date/time
        /// </summary>
        public static Tag Time(object attributes, Func<Tag> content)
        {
            return Time(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a date/time
        /// </summary>            
        public static Tag Time(Tag content)
        {
            return Time(new object(), content.Render());
        }

        /// <summary>
        /// Defines a date/time
        /// </summary>
        public static Tag Time(object attributes = null, string content = null)
        {
            return TagProvider("time", attributes, content);
        }

        /// <summary>
        /// Defines a title for the document    
        /// </summary>
        public static Tag Title(Func<Tag> content)
        {
            return Title(new object(), content);
        }

        /// <summary>
        /// Defines a title for the document
        /// </summary>
        public static Tag Title(object attributes, Func<Tag> content)
        {
            return Title(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a title for the document
        /// </summary>            
        public static Tag Title(Tag content)
        {
            return Title(new object(), content.Render());
        }

        /// <summary>
        /// Defines a title for the document
        /// </summary>
        public static Tag Title(object attributes = null, string content = null)
        {
            return TagProvider("title", attributes, content);
        }

        /// <summary>
        /// Defines a row in a table    
        /// </summary>
        public static Tag Tr(Func<Tag> content)
        {
            return Tr(new object(), content);
        }

        /// <summary>
        /// Defines a row in a table
        /// </summary>
        public static Tag Tr(object attributes, Func<Tag> content)
        {
            return Tr(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a row in a table
        /// </summary>            
        public static Tag Tr(Tag content)
        {
            return Tr(new object(), content.Render());
        }

        /// <summary>
        /// Defines a row in a table
        /// </summary>
        public static Tag Tr(object attributes = null, string content = null)
        {
            return TagProvider("tr", attributes, content);
        }

        /// <summary>
        /// Defines text tracks for media elements (&lt;video&gt; and &lt;audio&gt;)    
        /// </summary>
        public static Tag Track(Func<Tag> content)
        {
            return Track(new object(), content);
        }

        /// <summary>
        /// Defines text tracks for media elements (&lt;video&gt; and &lt;audio&gt;)
        /// </summary>
        public static Tag Track(object attributes, Func<Tag> content)
        {
            return Track(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines text tracks for media elements (&lt;video&gt; and &lt;audio&gt;)
        /// </summary>            
        public static Tag Track(Tag content)
        {
            return Track(new object(), content.Render());
        }

        /// <summary>
        /// Defines text tracks for media elements (&lt;video&gt; and &lt;audio&gt;)
        /// </summary>
        public static Tag Track(object attributes = null, string content = null)
        {
            return TagProvider("track", attributes, content);
        }

        /// <summary>
        /// Defines teletype text    
        /// </summary>
        public static Tag Tt(Func<Tag> content)
        {
            return Tt(new object(), content);
        }

        /// <summary>
        /// Defines teletype text
        /// </summary>
        public static Tag Tt(object attributes, Func<Tag> content)
        {
            return Tt(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines teletype text
        /// </summary>            
        public static Tag Tt(Tag content)
        {
            return Tt(new object(), content.Render());
        }

        /// <summary>
        /// Defines teletype text
        /// </summary>
        public static Tag Tt(object attributes = null, string content = null)
        {
            return TagProvider("tt", attributes, content);
        }

        /// <summary>
        /// Defines text that should be stylistically different from normal text    
        /// </summary>
        public static Tag U(Func<Tag> content)
        {
            return U(new object(), content);
        }

        /// <summary>
        /// Defines text that should be stylistically different from normal text
        /// </summary>
        public static Tag U(object attributes, Func<Tag> content)
        {
            return U(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines text that should be stylistically different from normal text
        /// </summary>            
        public static Tag U(Tag content)
        {
            return U(new object(), content.Render());
        }

        /// <summary>
        /// Defines text that should be stylistically different from normal text
        /// </summary>
        public static Tag U(object attributes = null, string content = null)
        {
            return TagProvider("u", attributes, content);
        }

        /// <summary>
        /// Defines an unordered list    
        /// </summary>
        public static Tag Ul(Func<Tag> content)
        {
            return Ul(new object(), content);
        }

        /// <summary>
        /// Defines an unordered list
        /// </summary>
        public static Tag Ul(object attributes, Func<Tag> content)
        {
            return Ul(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines an unordered list
        /// </summary>            
        public static Tag Ul(Tag content)
        {
            return Ul(new object(), content.Render());
        }

        /// <summary>
        /// Defines an unordered list
        /// </summary>
        public static Tag Ul(object attributes = null, string content = null)
        {
            return TagProvider("ul", attributes, content);
        }

        /// <summary>
        /// Defines a variable    
        /// </summary>
        public static Tag Var(Func<Tag> content)
        {
            return Var(new object(), content);
        }

        /// <summary>
        /// Defines a variable
        /// </summary>
        public static Tag Var(object attributes, Func<Tag> content)
        {
            return Var(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a variable
        /// </summary>            
        public static Tag Var(Tag content)
        {
            return Var(new object(), content.Render());
        }

        /// <summary>
        /// Defines a variable
        /// </summary>
        public static Tag Var(object attributes = null, string content = null)
        {
            return TagProvider("var", attributes, content);
        }

        /// <summary>
        /// Defines a video or movie    
        /// </summary>
        public static Tag Video(Func<Tag> content)
        {
            return Video(new object(), content);
        }

        /// <summary>
        /// Defines a video or movie
        /// </summary>
        public static Tag Video(object attributes, Func<Tag> content)
        {
            return Video(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a video or movie
        /// </summary>            
        public static Tag Video(Tag content)
        {
            return Video(new object(), content.Render());
        }

        /// <summary>
        /// Defines a video or movie
        /// </summary>
        public static Tag Video(object attributes = null, string content = null)
        {
            return TagProvider("video", attributes, content);
        }

        /// <summary>
        /// Defines a possible line-break    
        /// </summary>
        public static Tag Wbr(Func<Tag> content)
        {
            return Wbr(new object(), content);
        }

        /// <summary>
        /// Defines a possible line-break
        /// </summary>
        public static Tag Wbr(object attributes, Func<Tag> content)
        {
            return Wbr(attributes, content()?.Render());
        }

        /// <summary>
        /// Defines a possible line-break
        /// </summary>            
        public static Tag Wbr(Tag content)
        {
            return Wbr(new object(), content.Render());
        }

        /// <summary>
        /// Defines a possible line-break
        /// </summary>
        public static Tag Wbr(object attributes = null, string content = null)
        {
            return TagProvider("wbr", attributes, content);
        }
    }
}