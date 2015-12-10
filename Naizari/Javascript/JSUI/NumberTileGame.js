function Tile(){
    
    this.Number = 1;
    this.Spot = null;
    this.OldSpot = null;
    
    var daObj = this;
    
    this.Move = function(){
        if( daObj.Spot != null){
            if( daObj.Spot.NeighborSpots != null ){
                for(i = 0; i < daObj.Spot.NeighborSpots.length; i++){
                    if( daObj.Spot.NeighborSpots[i].IsEmpty() ){
                        daObj.OldSpot = daObj.Spot;
                        daObj.OldSpot.Tile = null;
                        daObj.OldSpot.Draw();
                        daObj.Spot = daObj.Spot.NeighborSpots[i];
                        daObj.Spot.Tile = daObj;
                        daObj.Spot.Draw();
                        if(daObj.OldSpot.ShouldBeEmpty){
                            daObj.OldSpot.CheckForWin();
                        }
                        break;
                    }
                }
            }            
        }
    }
}

function Spot(number){
    
    this.Tile = null;
    this.Number = number;
    this.Game = null; // this gets set by the NumberTileGame.Start() method
    this.ID = getRandomString(8);
    this.NeighborSpots = new Array();

    this.ShouldBeEmpty = false;
    
    var daObj = this;
    
    this.IsEmpty = function(){
        return daObj.Tile == null;
    }
    
    this.CheckForWin = function(){
        var rows = daObj.Game.Rows;
        var cols = daObj.Game.Cols;
        
        for(var row = rows - 1; row > -1; row--){
            for(var col = cols - 1; col > -1; col--){
                var nextSpot = daObj.Game.Spots[row][col];
                
                // if the spot should be empty and it is continue;
                if( nextSpot.ShouldBeEmpty && (nextSpot.Tile == null || nextSpot.Tile == 'undefined') )
                    continue;
                else if( nextSpot.ShouldBeEmpty && (nextSpot.Tile != null || nextSpot.Tile != 'undefined'))
                    return;
                
                // if the spot has a tile and the numbers match continue;
                if( nextSpot.Tile != null && nextSpot.Number == nextSpot.Tile.Number )
                    continue;
                else if( nextSpot.Tile != null && nextSpot.Number != nextSpot.Tile.Number )
                    return;
            }
        }
        
        alert("Conratulations! You win!");
    }
    
    this.Draw = function(){
        var element = document.getElementById(daObj.ID);
        var inner = daObj.IsEmpty() ? "": daObj.Tile.Number.toString();
        element.innerHTML = inner;
        element.style.border = "1px solid black";
        element.style.width = "25px";
        element.style.height = "25px";
        element.setAttribute("align", "center");
        if( daObj.Tile != null && daObj.Tile != 'undefined')
            element.onclick = daObj.Tile.Move;
    }
}

function NumberTileGame(intRows, intCols, boolDebug){
    this.Spots = new Array();
    this.AllTiles = new Array();
    
    this.Rows = intRows;
    this.Cols = intCols;
    this.ID = getRandomString(8);
    this.TableID = null;

    var daObj = this;
    
    this.Div = document.createElement("div");
    this.Div.setAttribute("id", this.ID);
    document.body.appendChild(this.Div);
        
    this.Restart = function(){
        var div = document.getElementById(daObj.ID);
        div.removeChild(document.getElementById(daObj.TableID));
        daObj.Start();
    }    

    this.Start = function(){
        var total = daObj.Rows * daObj.Cols;

        daObj.TableID = getRandomString(8);
        //make the tiles
        for(i=0; i< total -1; i++){
            var tile = new Tile();
            tile.Number = i + 1;
            daObj.AllTiles.push(tile);
        }
        //make the spots
        var spotNumber = 1;
        for(row=0; row < daObj.Rows; row++){
            daObj.Spots[row] = new Object();
            for(col=0; col< daObj.Cols; col++){
                var nextSpot = new Spot(spotNumber);
                nextSpot.Game = daObj;
                if( spotNumber == total )
                    nextSpot.ShouldBeEmpty = true;
                daObj.Spots[row][col] = nextSpot;
                    
                spotNumber++;
            }
        }
        //set the neigbors
        for(row = 0; row < daObj.Rows; row++){
            for(col=0; col< daObj.Cols; col++){
                if( isNotNull(daObj.Spots[row - 1]) && isNotNull(daObj.Spots[row - 1][col])){
                    daObj.Spots[row][col].NeighborSpots.push(daObj.Spots[row - 1][col]);
                }
                
                if( isNotNull(daObj.Spots[row][col - 1]) ){
                    daObj.Spots[row][col].NeighborSpots.push(daObj.Spots[row][col - 1]);
                }
                
                if( isNotNull(daObj.Spots[row][col + 1])){
                    daObj.Spots[row][col].NeighborSpots.push(daObj.Spots[row][col + 1]);
                }
                
                if( isNotNull(daObj.Spots[row + 1]) && isNotNull(daObj.Spots[row + 1][col])){
                    daObj.Spots[row][col].NeighborSpots.push(daObj.Spots[row + 1][col]);
                }
            }
        }
        
        //set the spots tile to the next one in order if we're debugging, a random one otherwise
        var tileCount = 0;
        for(row = 0; row <daObj.Rows; row++){
            for(col=0; col<daObj.Cols; col++){
                if(tileCount < (total - 1)){
                    var tile;
                    if( boolDebug ){
                        tile = getElementInOrder(daObj.AllTiles);
                    }else {
                        tile = getRandomElementFromArray(daObj.AllTiles);
                    }
                    tile.Spot = daObj.Spots[row][col];
                    daObj.Spots[row][col].Tile = tile;
                    tileCount++;
                }
            }
        }
        
        var table = document.createElement("table");
        table.style.border = "1px solid black";
        daObj.Div.appendChild(table);
        table.setAttribute("cellspacing", "2px");
        table.setAttribute("id", daObj.TableID);
        
        // Handle an IE rendering bug that causes it not to render a table
        // if rows are added to a table instead of a tbody inside the table
        if( document.all ){
            var tbody = document.createElement("TBODY");
            table.appendChild(tbody);
            table = tbody;
        }
            
        var cellNumber = 1;
        for(row = 0; row < daObj.Rows; row++){
            var tableRow = table.insertRow(-1);
            for(col=0; col< daObj.Cols; col++){
                var cell = tableRow.insertCell(col);
                var spot = daObj.Spots[row][col];
                cell.setAttribute("id", spot.ID);
                if( isNotNull(spot.Tile) ){
                    cell.innerHtml = spot.Tile.Number.toString();
                    cellNumber++;
                }
                tableRow.appendChild(cell);
            }
            table.appendChild(tableRow);
        }

        // Draw the tiles
        for(row = 0; row < daObj.Rows; row++){
            for(col=0; col<daObj.Cols; col++){
                daObj.Spots[row][col].Draw();
            }
        }
    }
    
    this.Shuffle = document.createElement("input");
    this.Shuffle.setAttribute("type", "button");
    this.Shuffle.onclick = this.Restart;
    this.Shuffle.value = "Shuffle";
    document.body.appendChild(document.createElement("br"));
    document.body.appendChild(this.Shuffle);
}

function getRandomElementFromArray(objArray)
{
    var num = Math.floor(Math.random() * objArray.length);
    var monkey = objArray.splice(num, 1)[0];
    return monkey;
}

function getRandomString(intCount){
    var alpha = new Array("a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z");
    var retVal = "";
    for(var i = 0; i < intCount; i++){
        var index = Math.floor(Math.random() * 26);
        retVal += alpha[index];
    }
    return retVal;
}

function getElementInOrder(objArray)
{
    var monkey = objArray.splice(0, 1)[0];
    return monkey;
}

function isNotNull(obj){
    return obj != null && obj != 'undefined';
}
