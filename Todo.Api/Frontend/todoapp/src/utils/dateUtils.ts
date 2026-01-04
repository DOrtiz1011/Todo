export const formatDateTimeForDisplay = (dateValue: any) => {
    if (!dateValue) return 'N/A';

    let dateStr = dateValue.toString();

    // If the string doesn't end with 'Z' and doesn't contain a '+' (offset),
    // append 'Z' to force JavaScript to treat it as UTC.
    if (!dateStr.endsWith('Z') && !dateStr.includes('+')) {
        dateStr += 'Z';
    }

    const date = new Date(dateStr);

    if (isNaN(date.getTime()) || date.getFullYear() <= 1) return 'Not Set';

    // Now format using the local system settings
    const dayName = new Intl.DateTimeFormat('en-US', { weekday: 'short' }).format(date);
    const day = new Intl.DateTimeFormat('en-US', { day: '2-digit' }).format(date);
    const month = new Intl.DateTimeFormat('en-US', { month: 'short' }).format(date);
    const year = new Intl.DateTimeFormat('en-US', { year: 'numeric' }).format(date);

    const timeParts = new Intl.DateTimeFormat('en-US', {
        hour: 'numeric',
        minute: '2-digit',
        hour12: true,
        timeZoneName: 'short'
    }).formatToParts(date);

    const hour = timeParts.find(p => p.type === 'hour')?.value;
    const minute = timeParts.find(p => p.type === 'minute')?.value;
    const period = timeParts.find(p => p.type === 'dayPeriod')?.value;
    const tz = timeParts.find(p => p.type === 'timeZoneName')?.value;

    return `${dayName} ${day}-${month}-${year} ${hour}:${minute} ${period} ${tz}`;
};

export const formatForInput = (date?: Date | string) => {
    if (!date) return '';
    const d = new Date(date);
    return d.toISOString().slice(0, 16);
};