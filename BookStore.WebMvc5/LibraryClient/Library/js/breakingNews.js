﻿!function (C) {
    "use strict";
    C.breakingNews = function (t, e) {
        var i = {
            effect: "scroll",
            direction: "ltr",
            height: 40,
            fontSize: "default",
            themeColor: "default",
            background: "default",
            borderWidth: 1,
            radius: 2,
            source: "html",
            rss2jsonApiKey: "",
            play: !0,
            delayTimer: 4e3,
            scrollSpeed: 2,
            stopOnHover: !0,
            position: "auto",
            zIndex: 99999
        }
            , o = this;
        o.settings = {};
        var s = C(t)
            , n = (t = t,
                s.children(".bn-label"))
            , l = s.children(".bn-news")
            , c = l.children("ul")
            , d = c.children("li")
            , r = s.children(".bn-controls")
            , f = r.find(".bn-prev").parent()
            , h = r.find(".bn-action").parent()
            , g = r.find(".bn-next").parent()
            , u = !1
            , p = !0
            , m = c.children("li").length
            , a = 0
            , b = !1
            , y = function () {
                if (0 < n.length && ("rtl" == o.settings.direction ? l.css({
                    right: n.outerWidth()
                }) : l.css({
                    left: n.outerWidth()
                })),
                    0 < r.length) {
                    var t = r.outerWidth();
                    "rtl" == o.settings.direction ? l.css({
                        left: t
                    }) : l.css({
                        right: t
                    })
                }
                if ("scroll" === o.settings.effect) {
                    var e = 0;
                    d.each(function () {
                        e += C(this).outerWidth()
                    }),
                        e += 10,
                        c.css({
                            width: e
                        })
                }
            }
            , k = function () {
                var t = parseFloat(c.css("marginLeft"));
                t -= o.settings.scrollSpeed / 2,
                    c.css({
                        marginLeft: t
                    }),
                    t <= -c.find("li:first-child").outerWidth() && (c.find("li:first-child").insertAfter(c.find("li:last-child")),
                        c.css({
                            marginLeft: 0
                        })),
                    !1 === u && (window.requestAnimationFrame && requestAnimationFrame(k) || setTimeout(k, 16))
            }
            , w = function () {
                var t = parseFloat(c.css("marginRight"));
                t -= o.settings.scrollSpeed / 2,
                    c.css({
                        marginRight: t
                    }),
                    t <= -c.find("li:first-child").outerWidth() && (c.find("li:first-child").insertAfter(c.find("li:last-child")),
                        c.css({
                            marginRight: 0
                        })),
                    !1 === u && (window.requestAnimationFrame && requestAnimationFrame(w) || setTimeout(w, 16))
            }
            , v = function () {
                "rtl" === o.settings.direction ? c.stop().animate({
                    marginRight: -c.find("li:first-child").outerWidth()
                }, 300, function () {
                    c.find("li:first-child").insertAfter(c.find("li:last-child")),
                        c.css({
                            marginRight: 0
                        }),
                        p = !0
                }) : c.stop().animate({
                    marginLeft: -c.find("li:first-child").outerWidth()
                }, 300, function () {
                    c.find("li:first-child").insertAfter(c.find("li:last-child")),
                        c.css({
                            marginLeft: 0
                        }),
                        p = !0
                })
            }
            , q = function () {
                "rtl" === o.settings.direction ? (0 <= parseInt(c.css("marginRight"), 10) && (c.css({
                    "margin-right": -c.find("li:last-child").outerWidth()
                }),
                    c.find("li:last-child").insertBefore(c.find("li:first-child"))),
                    c.stop().animate({
                        marginRight: 0
                    }, 300, function () {
                        p = !0
                    })) : (0 <= parseInt(c.css("marginLeft"), 10) && (c.css({
                        "margin-left": -c.find("li:last-child").outerWidth()
                    }),
                        c.find("li:last-child").insertBefore(c.find("li:first-child"))),
                        c.stop().animate({
                            marginLeft: 0
                        }, 300, function () {
                            p = !0
                        }))
            }
            , x = function () {
                switch (p = !0,
                o.settings.effect) {
                    case "typography":
                        c.find("li").hide(),
                            c.find("li").eq(a).width(30).show(),
                            c.find("li").eq(a).animate({
                                width: "100%",
                                opacity: 1
                            }, 1500);
                        break;
                    case "fade":
                        c.find("li").hide(),
                            c.find("li").eq(a).fadeIn();
                        break;
                    case "slide-down":
                        m <= 1 ? c.find("li").animate({
                            top: 30,
                            opacity: 0
                        }, 300, function () {
                            C(this).css({
                                top: -30,
                                opacity: 0,
                                display: "block"
                            }),
                                C(this).animate({
                                    top: 0,
                                    opacity: 1
                                }, 300)
                        }) : (c.find("li:visible").animate({
                            top: 30,
                            opacity: 0
                        }, 300, function () {
                            C(this).hide()
                        }),
                            c.find("li").eq(a).css({
                                top: -30,
                                opacity: 0
                            }).show(),
                            c.find("li").eq(a).animate({
                                top: 0,
                                opacity: 1
                            }, 300));
                        break;
                    case "slide-up":
                        m <= 1 ? c.find("li").animate({
                            top: -30,
                            opacity: 0
                        }, 300, function () {
                            C(this).css({
                                top: 30,
                                opacity: 0,
                                display: "block"
                            }),
                                C(this).animate({
                                    top: 0,
                                    opacity: 1
                                }, 300)
                        }) : (c.find("li:visible").animate({
                            top: -30,
                            opacity: 0
                        }, 300, function () {
                            C(this).hide()
                        }),
                            c.find("li").eq(a).css({
                                top: 30,
                                opacity: 0
                            }).show(),
                            c.find("li").eq(a).animate({
                                top: 0,
                                opacity: 1
                            }, 300));
                        break;
                    case "slide-left":
                        m <= 1 ? c.find("li").animate({
                            left: "50%",
                            opacity: 0
                        }, 300, function () {
                            C(this).css({
                                left: -50,
                                opacity: 0,
                                display: "block"
                            }),
                                C(this).animate({
                                    left: 0,
                                    opacity: 1
                                }, 300)
                        }) : (c.find("li:visible").animate({
                            left: "50%",
                            opacity: 0
                        }, 300, function () {
                            C(this).hide()
                        }),
                            c.find("li").eq(a).css({
                                left: -50,
                                opacity: 0
                            }).show(),
                            c.find("li").eq(a).animate({
                                left: 0,
                                opacity: 1
                            }, 300));
                        break;
                    case "slide-right":
                        m <= 1 ? c.find("li").animate({
                            left: "-50%",
                            opacity: 0
                        }, 300, function () {
                            C(this).css({
                                left: "50%",
                                opacity: 0,
                                display: "block"
                            }),
                                C(this).animate({
                                    left: 0,
                                    opacity: 1
                                }, 300)
                        }) : (c.find("li:visible").animate({
                            left: "-50%",
                            opacity: 0
                        }, 300, function () {
                            C(this).hide()
                        }),
                            c.find("li").eq(a).css({
                                left: "50%",
                                opacity: 0
                            }).show(),
                            c.find("li").eq(a).animate({
                                left: 0,
                                opacity: 1
                            }, 300));
                        break;
                    default:
                        c.find("li").hide(),
                            c.find("li").eq(a).show()
                }
            }
            , W = function () {
                if (u = !1,
                    o.settings.play)
                    switch (o.settings.effect) {
                        case "scroll":
                            "rtl" === o.settings.direction ? c.width() > l.width() && w() : c.width() > l.width() && k();
                            break;
                        default:
                            o.pause(),
                                b = setInterval(function () {
                                    o.next()
                                }, o.settings.delayTimer)
                    }
            };
        o.init = function () {
            if (o.settings = C.extend({}, i, e),
                "fixed-top" === o.settings.position ? s.addClass("bn-fixed-top").css({
                    "z-index": o.settings.zIndex
                }) : "fixed-bottom" === o.settings.position && s.addClass("bn-fixed-bottom").css({
                    "z-index": o.settings.zIndex
                }),
                "default" != o.settings.fontSize && s.css({
                    "font-size": o.settings.fontSize
                }),
                "default" != o.settings.themeColor && (s.css({
                    "border-color": o.settings.themeColor,
                    color: o.settings.themeColor
                }),
                    n.css({
                        background: o.settings.themeColor
                    })),
                "default" != o.settings.background && s.css({
                    background: o.settings.background
                }),
                s.css({
                    height: o.settings.height,
                    "line-height": o.settings.height - 2 * o.settings.borderWidth + "px",
                    "border-radius": o.settings.radius,
                    "border-width": o.settings.borderWidth
                }),
                d.find(".bn-seperator").css({
                    height: o.settings.height - 2 * o.settings.borderWidth
                }),
                s.addClass("bn-effect-" + o.settings.effect + " bn-direction-" + o.settings.direction),
                y(),
                "object" == typeof o.settings.source)
                switch (o.settings.source.type) {
                    case "rss":
                        "rss2json" === o.settings.source.usingApi ? ((a = new XMLHttpRequest).onreadystatechange = function () {
                            if (4 == a.readyState && 200 == a.status) {
                                var t = JSON.parse(a.responseText)
                                    , e = ""
                                    , i = "";
                                switch (o.settings.source.showingField) {
                                    case "title":
                                        i = "title";
                                        break;
                                    case "description":
                                        i = "description";
                                        break;
                                    case "link":
                                        i = "link";
                                        break;
                                    default:
                                        i = "title"
                                }
                                var s = "";
                                void 0 !== o.settings.source.seperator && void 0 !== typeof o.settings.source.seperator && (s = o.settings.source.seperator);
                                for (var n = 0; n < t.items.length; n++)
                                    o.settings.source.linkEnabled ? e += '<li><a target="' + o.settings.source.target + '" href="' + t.items[n].link + '">' + s + t.items[n][i] + "</a></li>" : e += "<li><a>" + s + t.items[n][i] + "</a></li>";
                                c.empty().append(e),
                                    d = c.children("li"),
                                    m = c.children("li").length,
                                    y(),
                                    "scroll" != o.settings.effect && x(),
                                    d.find(".bn-seperator").css({
                                        height: o.settings.height - 2 * o.settings.borderWidth
                                    }),
                                    W()
                            }
                        }
                            ,
                            a.open("GET", "https://api.rss2json.com/v1/api.json?rss_url=" + o.settings.source.url + "&count=" + o.settings.source.limit + "&api_key=" + o.settings.source.rss2jsonApiKey, !0),
                            a.send()) : ((r = new XMLHttpRequest).open("GET", "https://query.yahooapis.com/v1/public/yql?q=" + encodeURIComponent('select * from rss where url="' + o.settings.source.url + '" limit ' + o.settings.source.limit) + "&format=json", !0),
                                r.onreadystatechange = function () {
                                    if (4 == r.readyState)
                                        if (200 == r.status) {
                                            var t = JSON.parse(r.responseText)
                                                , e = ""
                                                , i = "";
                                            switch (o.settings.source.showingField) {
                                                case "title":
                                                    i = "title";
                                                    break;
                                                case "description":
                                                    i = "description";
                                                    break;
                                                case "link":
                                                    i = "link";
                                                    break;
                                                default:
                                                    i = "title"
                                            }
                                            var s = "";
                                            "undefined" != o.settings.source.seperator && void 0 !== o.settings.source.seperator && (s = o.settings.source.seperator);
                                            for (var n = 0; n < t.query.results.item.length; n++)
                                                o.settings.source.linkEnabled ? e += '<li><a target="' + o.settings.source.target + '" href="' + t.query.results.item[n].link + '">' + s + t.query.results.item[n][i] + "</a></li>" : e += "<li><a>" + s + t.query.results.item[n][i] + "</a></li>";
                                            c.empty().append(e),
                                                d = c.children("li"),
                                                m = c.children("li").length,
                                                y(),
                                                "scroll" != o.settings.effect && x(),
                                                d.find(".bn-seperator").css({
                                                    height: o.settings.height - 2 * o.settings.borderWidth
                                                }),
                                                W()
                                        } else
                                            c.empty().append('<li><span class="bn-loader-text">' + o.settings.source.errorMsg + "</span></li>")
                                }
                                ,
                                r.send(null));
                        break;
                    case "json":
                        C.getJSON(o.settings.source.url, function (t) {
                            var e = ""
                                , i = "";
                            i = "undefined" === o.settings.source.showingField ? "title" : o.settings.source.showingField;
                            var s = "";
                            void 0 !== o.settings.source.seperator && void 0 !== typeof o.settings.source.seperator && (s = o.settings.source.seperator);
                            for (var n = 0; n < t.length && !(n >= o.settings.source.limit); n++)
                                o.settings.source.linkEnabled ? e += '<li><a target="' + o.settings.source.target + '" href="' + t[n].link + '">' + s + t[n][i] + "</a></li>" : e += "<li><a>" + s + t[n][i] + "</a></li>",
                                    "undefined" === t[n][i] && console.log('"' + i + '" does not exist in this json.');
                            c.empty().append(e),
                                d = c.children("li"),
                                m = c.children("li").length,
                                y(),
                                "scroll" != o.settings.effect && x(),
                                d.find(".bn-seperator").css({
                                    height: o.settings.height - 2 * o.settings.borderWidth
                                }),
                                W()
                        });
                        break;
                    default:
                        console.log('Please check your "source" object parameter. Incorrect Value')
                }
            else
                "html" === o.settings.source ? ("scroll" != o.settings.effect && x(),
                    W()) : console.log('Please check your "source" parameter. Incorrect Value');
            var r, a;
            o.settings.play ? h.find("span").removeClass("bn-play").addClass("bn-pause") : h.find("span").removeClass("bn-pause").addClass("bn-play"),
                s.on("mouseleave", function (t) {
                    var e = C(document.elementFromPoint(t.clientX, t.clientY)).parents(".bn-breaking-news")[0];
                    C(this)[0] !== e && (!0 === o.settings.stopOnHover ? !0 === o.settings.play && o.play() : !0 === o.settings.play && !0 === u && o.play())
                }),
                s.on("mouseenter", function () {
                    !0 === o.settings.stopOnHover && o.pause()
                }),
                g.on("click", function () {
                    p && (p = !1,
                        o.pause(),
                        o.next())
                }),
                f.on("click", function () {
                    p && (p = !1,
                        o.pause(),
                        o.prev())
                }),
                h.on("click", function () {
                    p && (h.find("span").hasClass("bn-pause") ? (h.find("span").removeClass("bn-pause").addClass("bn-play"),
                        o.stop()) : (o.settings.play = !0,
                            h.find("span").removeClass("bn-play").addClass("bn-pause")))
                }),
                C(window).on("resize", function () {
                    s.width() < 480 ? (n.hide(),
                        "rtl" == o.settings.direction ? l.css({
                            right: 0
                        }) : l.css({
                            left: 0
                        })) : (n.show(),
                            "rtl" == o.settings.direction ? l.css({
                                right: n.outerWidth()
                            }) : l.css({
                                left: n.outerWidth()
                            }))
                })
        }
            ,
            o.pause = function () {
                u = !0,
                    clearInterval(b)
            }
            ,
            o.stop = function () {
                u = !0,
                    o.settings.play = !1
            }
            ,
            o.play = function () {
                W()
            }
            ,
            o.next = function () {
                !function () {
                    switch (o.settings.effect) {
                        case "scroll":
                            v();
                            break;
                        default:
                            m <= ++a && (a = 0),
                                x()
                    }
                }()
            }
            ,
            o.prev = function () {
                !function () {
                    switch (o.settings.effect) {
                        case "scroll":
                            q();
                            break;
                        default:
                            --a < 0 && (a = m - 1),
                                x()
                    }
                }()
            }
            ,
            o.init()
    }
        ,
        C.fn.breakingNews = function (e) {
            return this.each(function () {
                if (null == C(this).data("breakingNews")) {
                    var t = new C.breakingNews(this, e);
                    C(this).data("breakingNews", t)
                }
            })
        }
}(jQuery);