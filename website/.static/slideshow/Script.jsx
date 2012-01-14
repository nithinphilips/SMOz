/*
 * For a document with a "Background" layer and a number of other layers,
 * this this goes through each layer and save a png file with just the layer
 * and the background layer.
 */

#target photoshop

app.bringToFront();

if (app.documents.length == 0)
{
    var docRef = app.documents.add();
}
else
{
    var docRef = app.activeDocument;
}

var layers = docRef.layers;

if(layers.length < 2)
{
    alert("Need at least two layers");
}
else
{
    var outputFolder = Folder.selectDialog ("Select a folder to save the output files");

    if(outputFolder != null)
    {
        var backgroundLayer = docRef.layers.getByName("Background");

        for(i =0; i < layers.length; i++)
        {
            layers[i].visible = false;
        }
        backgroundLayer.visible = true;

        var pngOptions = new PNGSaveOptions();
        pngOptions.interlaced = true;

        for(i =0; i < layers.length; i++)
        {
            if(layers[i] != backgroundLayer)
            {
                layers[i].visible = true;
                docRef.saveAs(new File(outputFolder+ "/" + layers[i].name + ".png"), pngOptions, true);
                layers[i].visible = false;
            }
        }
    }else{
        alert("Error: No output folder chosen.");
    }
}
