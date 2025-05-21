export const base64ToBlob = (base64: string, mimeType: string): Blob => {
    const byteCharacters = atob(base64);
    const byteNumbers = new Array(byteCharacters.length);

    for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }

    const byteArray = new Uint8Array(byteNumbers);
    return new Blob([byteArray], { type: mimeType });
}

export const getMimeType = (typeReport: number): string => {
    let mimeType = 'application/octet-stream';

    if (typeReport == 1) {
        mimeType = 'text/csv';
    } else if (typeReport == 2) {
        mimeType = 'application/pdf';
    } else if (typeReport == 3) {
        mimeType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
    }

    return mimeType;
}