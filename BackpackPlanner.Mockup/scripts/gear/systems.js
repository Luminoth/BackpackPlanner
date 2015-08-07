function getGearSystemIndexById(gearSystems, gearSystemId) {
    for(var i=0; i<gearSystems.length; ++i) {
        var gearSystem = gearSystems[i];
        if(gearSystem.Id == gearSystemId) {
            return i;
        }
    }
    return -1;
}

function getGearSystemById(gearSystems, gearSystemId) {
    var idx = getGearSystemIndexById(gearSystems, gearSystemId);
    return idx < 0 ? null : gearSystems[idx];
}

function deleteGearSystem(gearSystems, gearSystem, gearCollections) {
    var idx = gearSystems.indexOf(gearSystem);
    if(idx < 0) {
        return false;
    }
    gearSystems.splice(idx, 1);

    // TODO: remove the system from the collections, and trip plans it belongs to

    return true;
}

function getGearSystemWeightInOunces(gearSystem) {
    var weightInOunces = 0;
    for(var i=0; i<gearSystem.GearItems.length; ++i) {
        // TODO: no reason to have this in the system JSON
        // we can just look it up from the full item list
        weightInOunces += gearSystem.GearItems[i].WeightInOunces;
    }
    return weightInOunces;
}

function getGearSystemCostInUSD(gearSystem) {
    var costInUSD = 0;
    for(var i=0; i<gearSystem.GearItems.length; ++i) {
        // TODO: no reason to have this in the system JSON
        // we can just look it up from the full item list
        costInUSD += gearSystem.GearItems[i].CostInUSD;
    }
    return costInUSD;
}
