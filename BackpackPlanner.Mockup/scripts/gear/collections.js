function getGearCollectionIndexById(gearCollections, gearCollectionId) {
    for(var i=0; i<gearCollections.length; ++i) {
        var gearCollection = gearCollections[i];
        if(gearCollection.Id == gearCollectionId) {
            return i;
        }
    }
    return -1;
}

function getGearCollectionById(gearCollections, gearCollectionId) {
    var idx = getGearCollectionIndexById(gearCollections, gearCollectionId);
    return idx < 0 ? null : gearCollections[idx];
}

function deleteGearCollection(gearCollections, gearCollection) {
    var idx = gearCollections.indexOf(gearCollection);
    if(idx < 0) {
        return false;
    }
    gearCollections.splice(idx, 1);

    // TODO: remove the collection from the trip plans it belongs to

    return true;
}
