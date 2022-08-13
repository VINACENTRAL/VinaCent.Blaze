class BlazeWebConstants {
    static PreviousAccount = 'PreviousAccount';
}

class AvailableRegexs {
    static EmailChecker = /^[\w\-\.]+@([\w-]+\.)+[\w-]{2,4}$/gm;
    static EmailListSeparateByCommaChecker = /([\w\-\.]+@([\w-]+\.)+[\w-]{2,4}),? ?/gm
}

const int = {
    MAX_VALUE: 2147483647, MIN_VALUE: -2147483648
};
const Guid = {
    EMPTY: '00000000-0000-0000-0000-000000000000'
}

class CommonTimer {
    static BlockElementTimeInSeconds = 30;
}