var createTeamOffCanvas;

window.showOffCanvas = (elementId) => {
    const offCanvasElement = document.querySelector(`#${elementId}`);

    if(!createTeamOffCanvas || createTeamOffCanvas._isShown === true){
        createTeamOffCanvas = new bootstrap.Offcanvas(offCanvasElement);
    }

    createTeamOffCanvas.show();
}

window.hideOffCanvas = (elementId) => {
    const offCanvasElement = document.querySelector(`#${elementId}`);

    if(!createTeamOffCanvas || createTeamOffCanvas._isShown === false){
        createTeamOffCanvas = new bootstrap.Offcanvas(offCanvasElement);
    }

    createTeamOffCanvas.hide();
}