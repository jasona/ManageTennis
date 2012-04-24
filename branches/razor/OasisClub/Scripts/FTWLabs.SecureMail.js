$(document).ready(function () {
    rot13map = rot13init();
});

$(document).ready(function () {
    $('a.email').each(function () {
        var e = this.rel.replace('/', '@');
        e = rot13(e);
        this.href = 'mailto:' + e;
        $(this).text(e);
    });
});

$(document).ready(function () {
    $('a.emailhrefonly').each(function () {
        var e = this.rel.replace('/', '@');
        e = rot13(e);
        this.href = 'mailto:' + e;
    });
});

// REST OF FILE IS PUBLIC DOMAIN
// This script hereby is dedicated in the Public Domain
// as long as nobody else claims the copyright for it.
// origin: 2000-01-08 nospam@geht.net http://tools.geht.net/rot13.html
// Use at own risk.

var rot13map;

function rot13init() {
    var map = new Array();
    var s = "abcdefghijklmnopqrstuvwxyz";

    for (i = 0; i < s.length; i++)
        map[s.charAt(i)] = s.charAt((i + 13) % 26);
    for (i = 0; i < s.length; i++)
        map[s.charAt(i).toUpperCase()] = s.charAt((i + 13) % 26).toUpperCase();
    return map;
}

function rot13(a) {
    if (!rot13map)
        rot13map = rot13init();
    s = "";
    for (i = 0; i < a.length; i++) {
        var b = a.charAt(i);

        s += (b >= 'A' && b <= 'Z' || b >= 'a' && b <= 'z' ? rot13map[b] : b);
    }
    return s;
}