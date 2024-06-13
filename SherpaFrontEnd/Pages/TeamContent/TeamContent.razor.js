/**
 * @param{string} filename
 * @param{string} contentType
 * @param{Blob} fileData
 */
function downloadFile(filename, contentType, fileData) {
    const file = new File([fileData], filename, {type: contentType});
    const downloadUrl = URL.createObjectURL(file);

    const downloadLink = document.createElement("a");
    document.body.appendChild(downloadLink);
    downloadLink.href = downloadUrl;
    downloadLink.target = "_self";
    downloadLink.click();

    URL.revokeObjectURL(downloadUrl);
    downloadLink.remove();
}