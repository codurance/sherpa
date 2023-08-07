var createTeamOffCanvas;

window.showOffCanvas = (elementId) => {
    const offCanvasElement = document.querySelector(`#${elementId}`);
    
    if(!createTeamOffCanvas){
        createTeamOffCanvas = new bootstrap.Offcanvas(offCanvasElement);
    }

    createTeamOffCanvas.show();
}

window.hideOffCanvas = (elementId) => {
    const offCanvasElement = document.querySelector(`#${elementId}`);
    
    if(!createTeamOffCanvas){
        createTeamOffCanvas = new bootstrap.Offcanvas(offCanvasElement);
    }

    createTeamOffCanvas.hide();
}