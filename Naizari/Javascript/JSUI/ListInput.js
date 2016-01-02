var ListInput = {};


ListInput.New = function(strContainerIdOrElement){
    return new ListInputClass(strContainerIdOrElement);  
}

ListInput.Instances = [];

function ListInputClass(strElementToRenderAsChildOfOrId){
    var parentElement = JSUI.GetElement(strElementToRenderAsChildOfOrId);
    var table = document.createElement("table");
    this.DeleteImage = null;
    this.RemoveRowText = "remove row";
    this.Style = table.style;
    this.RowStyle = null;
    this.CellStyle = null;
    this.id = JSUI.GetRandomId(8);
    this.Items = [];
    this.ItemsByDataId = {};
    this.ItemsByRowId = {};
    parentElement.ListInput = this;
    ListInput.Instances[this.id] = this;
    
    table.setAttribute("id", this.id);
    parentElement.appendChild(table);
    
     if(document.all){
        var thead = document.createElement("thead");
        table.appendChild(thead);
        var tbody = document.createElement("tbody");
        table.appendChild(tbody);
        table = tbody;
    }
    
    var thisObj = this;

    this.AddItem = function(dataItem) {
        if (thisObj.ItemsByDataId[dataItem.DataId] !== null && thisObj.ItemsByDataId[dataItem.DataId] !== undefined)
            return;

        var tr = document.createElement("tr");
        tr.setAttribute("dataId", dataItem.DataId);
        var rowId = JSUI.GetRandomId(8);
        tr.setAttribute("id", rowId);
        if (thisObj.RowStyle) {
            for (prop in thisObj.RowStyle) {
                tr.style[prop] = thisObj.RowStyle[prop];
            }
        }

        var td = document.createElement("td");
        var text = document.createTextNode(dataItem.DisplayName);
        td.appendChild(text);
        var delTd = document.createElement("td");
        delTd.align = "right";
        if (thisObj.CellStyle) {
            for (prop in thisObj.CellStyle) {
                td.style[prop] = thisObj.CellStyle[prop];
                delTd.style[prop] = thisObj.CellStyle[prop];
            }
        }
        var removeCommand = "ListInput.Instances['" + thisObj.id + "'].RemoveItem('" + rowId + "')";
        var delLink;
        if (thisObj.DeleteImage) {
            delLink = document.createElement("img");
            delLink.setAttribute("src", JSUI.Images[thisObj.DeleteImage].src);
            delLink.setAttribute("onclick", removeCommand);
        } else {
            delLink = document.createElement("a");
            delLink.setAttribute("href", "javascript:" + removeCommand);
            delLink.appendChild(document.createTextNode(thisObj.RemoveRowText));
        }

        delTd.appendChild(delLink);
        tr.appendChild(td);
        tr.appendChild(delTd);
        table.appendChild(tr);
        dataItem.RowId = rowId;
        thisObj.Items.push(dataItem);
        thisObj.ItemsByDataId[dataItem.DataId] = dataItem;
        thisObj.ItemsByRowId[rowId] = dataItem;
    }
    
    function removeItemFromArray(strDataId){
        for(var i = 0 ; i < thisObj.Items.length;i++){
            if(thisObj.Items[i].DataId == strDataId){
                thisObj.Items.splice(i, 1);
            }
        }
    }
    
    this.RemoveItem = function(strDataId){
        var tr;
        var data;
        if(thisObj.Items[strDataId]){
            data = thisObj.ItemsByDataId[strDataId]
            tr = JSUI.GetElement(data.RowId);
        }else{
            data = thisObj.ItemsByRowId[strDataId];
            tr = JSUI.GetElement(data.RowId);
        }
        table.removeChild(tr);        
        delete thisObj.ItemsByDataId[data.DataId];
        delete thisObj.ItemsByRowId[data.RowId]; 
        removeItemFromArray(data.DataId);       
    }
    
    this.Clear = function(){
        for(var i = 0; i < thisObj.Items.length; i++){
            thisObj.RemoveItem(thisObj.Items[i].DataId);
        }
    }
}

