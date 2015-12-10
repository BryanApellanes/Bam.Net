if(JSUI == null || JSUI == 'undefined')
    alert("The core JSUI.js file was not loaded");

var Paginator = {};
Paginator.Books = {};
Paginator.Book = function(intColumnsPerPage, intItemsPerColumn) {
    this.BookId = JSUI.RandomString(4);
    Paginator.Books[this.BookId] = this;

    this.Pages = [];
    this.CurrentPage = null;
    this.CurrentPageIndex = 0;
    this.ItemTagName = "div";

    this.ColumnsPerPage = intColumnsPerPage ? intColumnsPerPage : 3;
    this.ItemsPerColumn = intItemsPerColumn ? intItemsPerColumn : 15;

    var book = this;
    var createPage = function(intColumnsPerPage, intItemsPerColumn) {
        var page = document.createElement("div");
        page.setAttribute("class", "paginatorPage");
        page.setAttribute("className", "paginatorPage"); // because IE 7 sux
        page.ItemsPerColumn = intItemsPerColumn;
        page.Columns = [];
        page.CurrentColumnIndex = 0;
        page.CurrentColumn = null;

        var columnsTable = document.createElement("table");
        columnsTable.style.width = "100%";
        if (document.all) {
            var tbody = document.createElement("tbody");
            columnsTable.appendChild(tbody);
            page.appendChild(columnsTable);
            columnsTable = tbody;
        } else {
            page.appendChild(columnsTable);
        }

        var columnsRow = document.createElement("tr");
        columnsTable.appendChild(columnsRow);

        for (var i = 0; i < intColumnsPerPage; i++) {
            var column = document.createElement("td");
            column.setAttribute("class", "paginatorColumn");
            column.setAttribute("className", "paginatorColumn"); //bcuz IE7 sux
            column.Items = [];
            column.AddItem = function(item) {
                // "this" should now be a reference to the column
                if (column.Items.length < intItemsPerColumn) {
                    var clone = item.cloneNode(true);
                    clone.id = clone.id + "_pageItem";
                    this.appendChild(clone);
                    this.Items.push(clone);
                }
            }
            columnsRow.appendChild(column);
            page.Columns.push(column);
        }

        page.GetCurrentColumn = function() {
            if (this.CurrentColumn == null) {
                this.CurrentColumn = this.Columns[this.CurrentColumnIndex];
            }

            if (this.CurrentColumn.Items.length >= this.ItemsPerColumn) {
                this.CurrentColumnIndex++;
                if (this.CurrentColumnIndex > book.ColumnsPerPage - 1) {
                    var newPage = createPage(intColumnsPerPage, intItemsPerColumn);
                    book.CurrentPage = newPage;
                    return newPage.GetCurrentColumn();
                } else {
                    this.CurrentColumn = this.Columns[this.CurrentColumnIndex];
                }
            }
            return this.CurrentColumn;
        }

        page.AddItem = function(item) {
            var column = this.GetCurrentColumn();
            column.AddItem(item);
        }

        book.Pages.push(page);
        return page;
    }

    this.AddItem = function(item) {
        var page = null;
        if (book.Pages.length == 0) {
            page = createPage(book.ColumnsPerPage, book.ItemsPerColumn);
            book.CurrentPage = page;
        }

        page = book.CurrentPage;
        page.AddItem(item);
    }

    this.ShowPage = function(pageIndex) {
        if (book.Pages[pageIndex]) {
            //book.Pages[pageIndex].style.display = "block";
            if (book.CurrentPage)
                book.CurrentPage.style.display = "none";

            book.CurrentPageIndex = pageIndex;
            book.CurrentPage = book.Pages[pageIndex];
            book.CurrentPage.style.display = "block";
        }
    }

    this.NextPage = function() {
        book.ShowPage(++book.CurrentPageIndex);
    }

    this.PreviousPage = function() {
        book.ShowPage(--book.CurrentPageIndex);
    }

    this.Initialize = function(parentIdOrElement, strItemTagName) {
        var newContainer = document.createElement("div");
        if (strItemTagName)
            book.ItemTagName = strItemTagName;

        var targetContainer = JSUI.GetElement(parentIdOrElement);
        var targetItems = targetContainer.getElementsByTagName(book.ItemTagName);
        for (var i = 0; i < targetItems.length; i++) {
            book.AddItem(targetItems[i]);
        }

        for (var i = 0; i < book.Pages.length; i++) {
            var page = book.Pages[i];
            var currentPageNum = i + 1;
            var pageNumber = document.createTextNode("page " + (currentPageNum) + " of " + book.Pages.length);
            var pageNumberDiv = document.createElement("div");
            pageNumberDiv.setAttribute("class", "paginatorPageNumber");
            pageNumberDiv.setAttribute("className", "paginatorPageNumber"); //bcuz IE sux
            pageNumberDiv.appendChild(pageNumber);
            page.appendChild(pageNumberDiv);

            var navButtons = document.createElement("table");
            if (document.all) {
                var tbody = document.createElement("tbody");
                navButtons.appendChild(tbody);
                page.appendChild(navButtons);
                navButtons = tbody;
            } else {
                page.appendChild(navButtons);
            }

            navButtons.setAttribute("class", "paginatorPageNumbers");
            navButtons.setAttribute("className", "paginatorPageNumbers");
            var navRow = document.createElement("tr");
            navButtons.appendChild(navRow);

            var previousCell = document.createElement("td");
            previousCell.setAttribute("class", "paginatorNavPrevious");
            previousCell.setAttribute("className", "paginatorNavPrevious");
            var centerCell = document.createElement("td");
            centerCell.setAttribute("class", "paginatorNavCenter");
            centerCell.setAttribute("className", "paginatorNavCenter");
            var nextCell = document.createElement("td");
            nextCell.setAttribute("class", "paginatorNavNext");
            nextCell.setAttribute("className", "paginatorNavNext");

            navRow.appendChild(previousCell);
            navRow.appendChild(centerCell);
            navRow.appendChild(nextCell);

            if (i != 0) {
                var previousLink = document.createElement("a");
                previousLink.appendChild(document.createTextNode("<<"));
                JSUI.AddEventHandler(previousLink, book.PreviousPage, "click");
                previousCell.appendChild(previousLink);
                JSUI.SetHandCursor(previousLink);
            } else {
                previousCell.innerHTML = "&nbsp;";
            }

            var pageIndexString = i.toString();
            pageTens = 0;
            if (pageIndexString.length > 1) {
                var temp = pageIndexString.substring(pageIndexString.length - 2, pageIndexString.length - 1);
                pageTens = parseInt(temp);
            }
            pageTens = pageTens * 10;

            for (var ii = 1; ii <= 10; ii++) {
                var pageNum = (pageTens + ii);
                var linkIndex = pageNum - 1;
                var currentPageIndex = currentPageNum - 1;
                if (pageNum < book.Pages.length + 1) {
                    var pageLink = document.createElement("a");

                    JSUI.SetInnerText(pageLink, pageNum.toString());
                    if (linkIndex != currentPageIndex) {
                        //pageLink.setAttribute("onclick", "javascript:Paginator.Books['" + book.BookId + "'].ShowPage(" + linkIndex + ");");
                        pageLink.setAttribute("href", "javascript:Paginator.Books['" + book.BookId + "'].ShowPage(" + linkIndex + ");");
                        pageLink.setAttribute("class", "paginatorPageLink");
                        pageLink.setAttribute("className", "paginatorPageLink");
                        JSUI.SetHandCursor(pageLink);
                    } else {
                        pageLink.setAttribute("class", "paginatorPageLinkCurrent");
                        pageLink.setAttribute("className", "paginatorPageLinkCurrent");
                    }
                    centerCell.appendChild(pageLink);

                }
            }

            if (i != book.Pages.length - 1) {
                var nextLink = document.createElement("a");
                nextLink.appendChild(document.createTextNode(">>"));
                JSUI.AddEventHandler(nextLink, book.NextPage, "click");
                nextCell.appendChild(nextLink);
                JSUI.SetHandCursor(nextLink);
            } else {
                nextCell.innerHTML = "&nbsp;";
            }

            page.style.display = "none";
            newContainer.appendChild(page);
        }
        var d = document.createElement("div");
        targetContainer.parentNode.insertBefore(newContainer, targetContainer);
        targetContainer.parentNode.removeChild(targetContainer);
        if (book.Pages.length > 0) {
            book.Pages[0].style.display = "block";
            book.CurrentPage = book.Pages[0];
        }
    }
}