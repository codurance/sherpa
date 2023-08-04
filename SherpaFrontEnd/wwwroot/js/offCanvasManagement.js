window.showOffCanvas = (elementId) => {
    const offCanvasElement = document.querySelector(`#${elementId}`);
    const bootstrapOffCanvasElement = new bootstrap.Offcanvas(offCanvasElement);
    bootstrapOffCanvasElement.show();
}

window.hideOffCanvas = (elementId) => {
    const offCanvasElement = document.querySelector(`#${elementId}`);
    const bootstrapOffCanvasElement = new bootstrap.Offcanvas(offCanvasElement);
    bootstrapOffCanvasElement.hide();
}