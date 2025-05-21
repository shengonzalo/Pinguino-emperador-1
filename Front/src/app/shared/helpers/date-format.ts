export const MY_DATE_FORMAT = {
    parse: {
        dateInput: 'DD/MM/YYYY',
    },
    display: {
        dateInput: 'DD/MM/YYYY',
        monthYearLabel: 'MMM YYYY',
        dateA11yLabel: 'DD/MM/YYYY',
        monthYearA11yLabel: 'MMM YYYY',
    },
};

export const DATE_FORMAT = {
    default: 'DD/MM/YYYY',
    sqlServer: 'yyyy-MM-dd HH:mm:ss',
    long: 'dd/MM/yyyy HH:mm:ss.fff',
    formatted: 'dd-MM-yyyy  HH:mm:ss'
};

export const stringToDate = (dataStr: string, format: string = DATE_FORMAT.default) => {

    let year;
    let month;
    let day;
    let hour;
    let minute;
    let seconds;

    if (format == DATE_FORMAT.default || format == DATE_FORMAT.long) {

        hour = 0;
        minute = 0;
        seconds = 0;

        if (format == DATE_FORMAT.long) {
            let auxArray = dataStr.split(' ');
            dataStr = auxArray[0];

            let hourArray = auxArray[1];
            let auxHour = hourArray.split('.');
            let hours = auxHour[0].split(':');

            hour = parseInt(hours[0]);
            minute = parseInt(hours[1]);
            seconds = parseInt(hours[2]);
        }

        let dateArray = dataStr.split('/');

        year = parseInt(dateArray[2]);
        month = parseInt(dateArray[1]) - 1;
        day = parseInt(dateArray[0]);
    }
    else if (format == DATE_FORMAT.sqlServer) {

        let auxArray = dataStr.split(' ');
        let dateArray = auxArray[0].split('-');
        let hourArray = auxArray[1].split(':');

        year = parseInt(dateArray[0]);
        month = parseInt(dateArray[1]) - 1;
        day = parseInt(dateArray[2]);
        hour = parseInt(hourArray[0]);
        minute = parseInt(hourArray[1]);
        seconds = parseInt(hourArray[2]);
    }
    else {
        return new Date();
    }

    let date = new Date(year, month, day, hour, minute, seconds);

    return date;
};

export const dateToString = (date: Date, format: string = DATE_FORMAT.default, noTime: boolean = false): string => {

    let result = '';

    if (!date) {
        return result;
    }

    let dd = date.getDate();
    let mm = date.getMonth() + 1;
    let yyyy = date.getFullYear();

    let ddString = dd.toString().padStart(2, '0');
    let mmString = mm.toString().padStart(2, '0');

    let hh = date.getHours();
    let mms = date.getMinutes();
    let ss = date.getSeconds();
    let fff = date.getMilliseconds();

    let hhString = hh.toString().padStart(2, '0');
    let mmsString = mms.toString().padStart(2, '0');
    let ssString = ss.toString().padStart(2, '0');
    let fffString = fff.toString().padStart(3, '0');

    if (format == DATE_FORMAT.default) {
        result = `${ddString}/${mmString}/${yyyy}`;
    }
    else if (format == DATE_FORMAT.sqlServer) {
        result = noTime ? `${yyyy}-${mmString}-${ddString} 00:00:00` : `${yyyy}-${mmString}-${ddString} ${hhString}:${mmsString}:${ssString}`;
    }
    else if (format == DATE_FORMAT.long) {
        result = `${ddString}/${mmString}/${yyyy} ${hhString}:${mmsString}:${ssString}.${fffString}`;
    }
    else if (format == DATE_FORMAT.formatted) {
        result = noTime ? `${ddString}-${mmString}-${yyyy} 00:00:00` : `${ddString}-${mmString}-${yyyy} ${hhString}:${mmsString}:${ssString}`;
    }

    return result;
};

export const diffMins = (beg: Date, end: Date) => {
    let diff = (end.getTime() - beg.getTime()) / 1000;
    diff /= 60;
    return Math.abs(Math.round(diff));
};

export const diffSecs = (beg: Date, end: Date) => {
    let diff = (end.getTime() - beg.getTime()) / 1000;
    return Math.abs(Math.round(diff));
};