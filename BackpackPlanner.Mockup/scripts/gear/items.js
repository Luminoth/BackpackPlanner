function getGearItemIndexById(gearItems, gearItemId) {
    for(var i=0; i<gearItems.length; ++i) {
        var gearItem = gearItems[i];
        if(gearItem.Id == gearItemId) {
            return i;
        }
    }
    return -1;
}

function getGearItemById(gearItems, gearItemId) {
    var idx = getGearItemIndexById(gearItems, gearItemId);
    return idx < 0 ? null : gearItems[idx];
}

function deleteGearItem(gearItems, gearSystems, gearCollections, gearItem) {
    var idx = getGearItemIndexById(gearItems, gearItem.Id);
    if(idx < 0) {
        return false;
    }
    gearItems.splice(idx, 1);

    // TODO: remove the item from the systems, collections, and trip plans it belongs to

    return true;
}
