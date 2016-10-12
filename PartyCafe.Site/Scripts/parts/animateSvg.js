/*! vlogitcontrol 04-05-2014 by Marco Rosella www.marcorosella.com */
function openAbout() {
    $("#about h2").fadeIn(1e3, function() {
        var a = animtop.path("M " + (videoboxpos.left + 112) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 112) + "," + (videoboxpos.top + 45)).attr({
            fill: "none",
            "stroke-width": "1px",
            stroke: "#e7e7e7"
        });
        a.animate({
            path: "M " + (videoboxpos.left + 112) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 560) + "," + (videoboxpos.top + 45)
        }, 200, ">", function() {
            a.animate({
                path: "M " + (videoboxpos.left + 112) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 560) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 560) + "," + (videoboxpos.top + 300)
            }, 200, ">", function() {
                a.animate({
                    path: "M " + (videoboxpos.left + 112) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 560) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 560) + "," + (videoboxpos.top + 300) + " L " + (videoboxpos.left + 30) + "," + (videoboxpos.top + 300)
                }, 200, ">", function() {
                    a.animate({
                        path: "M " + (videoboxpos.left + 112) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 560) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 560) + "," + (videoboxpos.top + 300) + " L " + (videoboxpos.left + 30) + "," + (videoboxpos.top + 300) + " L " + (videoboxpos.left + 30) + "," + (videoboxpos.top + 45)
                    }, 200, ">", function() {
                        a.animate({
                            path: "M " + (videoboxpos.left + 112) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 560) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 560) + "," + (videoboxpos.top + 300) + " L " + (videoboxpos.left + 30) + "," + (videoboxpos.top + 300) + " L " + (videoboxpos.left + 30) + "," + (videoboxpos.top + 45) + " L " + (videoboxpos.left + 40) + "," + (videoboxpos.top + 45)
                        }, 200, ">", function() {
                            $("#about #about_content").fadeIn(1e3)
                        })
                    })
                })
            })
        })
    })
}

function endAbout() {}
function embedVideo(a) {
    $("#vimejo").html(unescape(a.html)),
    $("#descr h3").delay(4e3).animate({
        left: "0"
    }, 2e3, function() {
        $(this).delay(4e3).animate({
            left: -420
        }, 2e3)
    }),
    $("#descr h4").delay(4500).animate({
        left: "0"
    }, 2e3, function() {
        $(this).delay(3500).animate({
            left: -420
        }, 2e3)
    })
}

window.Modernizr = function(a, b, c) {
    function d(a) {
        n.cssText = a
    }
    function e(a, b) {
        return typeof a === b
    }
    var f, g, h, i = "2.8.0", j = {}, k = b.documentElement, l = "modernizr", m = b.createElement(l), n = m.style, o = ({}.toString,
    {}), p = [], q = p.slice, r = {}.hasOwnProperty;
    h = e(r, "undefined") || e(r.call, "undefined") ? function(a, b) {
        return b in a && e(a.constructor.prototype[b], "undefined")
    }
    : function(a, b) {
        return r.call(a, b)
    }
    ,
    Function.prototype.bind || (Function.prototype.bind = function(a) {
        var b = this;
        if ("function" != typeof b)
            throw new TypeError;
        var c = q.call(arguments, 1)
          , d = function() {
            if (this instanceof d) {
                var e = function() {}
                ;
                e.prototype = b.prototype;
                var f = new e
                  , g = b.apply(f, c.concat(q.call(arguments)));
                return Object(g) === g ? g : f
            }
            return b.apply(a, c.concat(q.call(arguments)))
        }
        ;
        return d
    }
    );
    for (var s in o)
        h(o, s) && (g = s.toLowerCase(),
        j[g] = o[s](),
        p.push((j[g] ? "" : "no-") + g));
    return j.addTest = function(a, b) {
        if ("object" == typeof a)
            for (var d in a)
                h(a, d) && j.addTest(d, a[d]);
        else {
            if (a = a.toLowerCase(),
            j[a] !== c)
                return j;
            b = "function" == typeof b ? b() : b,
            "undefined" != typeof enableClasses && enableClasses && (k.className += " " + (b ? "" : "no-") + a),
            j[a] = b
        }
        return j
    }
    ,
    d(""),
    m = f = null ,
    j._version = i,
    j
}(this, this.document),
function(a, b, c) {
    function d(a) {
        return "[object Function]" == q.call(a)
    }
    function e(a) {
        return "string" == typeof a
    }
    function f() {}
    function g(a) {
        return !a || "loaded" == a || "complete" == a || "uninitialized" == a
    }
    function h() {
        var a = r.shift();
        s = 1,
        a ? a.t ? o(function() {
            ("c" == a.t ? m.injectCss : m.injectJs)(a.s, 0, a.a, a.x, a.e, 1)
        }, 0) : (a(),
        h()) : s = 0
    }
    function i(a, c, d, e, f, i, j) {
        function k(b) {
            if (!n && g(l.readyState) && (t.r = n = 1,
            !s && h(),
            l.onload = l.onreadystatechange = null ,
            b)) {
                "img" != a && o(function() {
                    v.removeChild(l)
                }, 50);
                for (var d in A[c])
                    A[c].hasOwnProperty(d) && A[c][d].onload()
            }
        }
        var j = j || m.errorTimeout
          , l = b.createElement(a)
          , n = 0
          , q = 0
          , t = {
            t: d,
            s: c,
            e: f,
            a: i,
            x: j
        };
        1 === A[c] && (q = 1,
        A[c] = []),
        "object" == a ? l.data = c : (l.src = c,
        l.type = a),
        l.width = l.height = "0",
        l.onerror = l.onload = l.onreadystatechange = function() {
            k.call(this, q)
        }
        ,
        r.splice(e, 0, t),
        "img" != a && (q || 2 === A[c] ? (v.insertBefore(l, u ? null : p),
        o(k, j)) : A[c].push(l))
    }
    function j(a, b, c, d, f) {
        return s = 0,
        b = b || "j",
        e(a) ? i("c" == b ? x : w, a, b, this.i++, c, d, f) : (r.splice(this.i++, 0, a),
        1 == r.length && h()),
        this
    }
    function k() {
        var a = m;
        return a.loader = {
            load: j,
            i: 0
        },
        a
    }
    var l, m, n = b.documentElement, o = a.setTimeout, p = b.getElementsByTagName("script")[0], q = {}.toString, r = [], s = 0, t = "MozAppearance"in n.style, u = t && !!b.createRange().compareNode, v = u ? n : p.parentNode, n = a.opera && "[object Opera]" == q.call(a.opera), n = !!b.attachEvent && !n, w = t ? "object" : n ? "script" : "img", x = n ? "script" : w, y = Array.isArray || function(a) {
        return "[object Array]" == q.call(a)
    }
    , z = [], A = {}, B = {
        timeout: function(a, b) {
            return b.length && (a.timeout = b[0]),
            a
        }
    };
    m = function(a) {
        function b(a) {
            var b, c, d, a = a.split("!"), e = z.length, f = a.pop(), g = a.length, f = {
                url: f,
                origUrl: f,
                prefixes: a
            };
            for (c = 0; g > c; c++)
                d = a[c].split("="),
                (b = B[d.shift()]) && (f = b(f, d));
            for (c = 0; e > c; c++)
                f = z[c](f);
            return f
        }
        function g(a, e, f, g, h) {
            var i = b(a)
              , j = i.autoCallback;
            i.url.split(".").pop().split("?").shift(),
            i.bypass || (e && (e = d(e) ? e : e[a] || e[g] || e[a.split("/").pop().split("?")[0]]),
            i.instead ? i.instead(a, e, f, g, h) : (A[i.url] ? i.noexec = !0 : A[i.url] = 1,
            f.load(i.url, i.forceCSS || !i.forceJS && "css" == i.url.split(".").pop().split("?").shift() ? "c" : c, i.noexec, i.attrs, i.timeout),
            (d(e) || d(j)) && f.load(function() {
                k(),
                e && e(i.origUrl, h, g),
                j && j(i.origUrl, h, g),
                A[i.url] = 2
            })))
        }
        function h(a, b) {
            function c(a, c) {
                if (a) {
                    if (e(a))
                        c || (l = function() {
                            var a = [].slice.call(arguments);
                            m.apply(this, a),
                            n()
                        }
                        ),
                        g(a, l, b, 0, j);
                    else if (Object(a) === a)
                        for (i in h = function() {
                            var b, c = 0;
                            for (b in a)
                                a.hasOwnProperty(b) && c++;
                            return c
                        }(),
                        a)
                            a.hasOwnProperty(i) && (!c && !--h && (d(l) ? l = function() {
                                var a = [].slice.call(arguments);
                                m.apply(this, a),
                                n()
                            }
                            : l[i] = function(a) {
                                return function() {
                                    var b = [].slice.call(arguments);
                                    a && a.apply(this, b),
                                    n()
                                }
                            }(m[i])),
                            g(a[i], l, b, i, j))
                } else
                    !c && n()
            }
            var h, i, j = !!a.test, k = a.load || a.both, l = a.callback || f, m = l, n = a.complete || f;
            c(j ? a.yep : a.nope, !!k),
            k && c(k)
        }
        var i, j, l = this.yepnope.loader;
        if (e(a))
            g(a, 0, l, 0);
        else if (y(a))
            for (i = 0; i < a.length; i++)
                j = a[i],
                e(j) ? g(j, 0, l, 0) : y(j) ? m(j) : Object(j) === j && h(j, l);
        else
            Object(a) === a && h(a, l)
    }
    ,
    m.addPrefix = function(a, b) {
        B[a] = b
    }
    ,
    m.addFilter = function(a) {
        z.push(a)
    }
    ,
    m.errorTimeout = 1e4,
    null == b.readyState && b.addEventListener && (b.readyState = "loading",
    b.addEventListener("DOMContentLoaded", l = function() {
        b.removeEventListener("DOMContentLoaded", l, 0),
        b.readyState = "complete"
    }
    , 0)),
    a.yepnope = k(),
    a.yepnope.executeStack = h,
    a.yepnope.injectJs = function(a, c, d, e, i, j) {
        var k, l, n = b.createElement("script"), e = e || m.errorTimeout;
        n.src = a;
        for (l in d)
            n.setAttribute(l, d[l]);
        c = j ? h : c || f,
        n.onreadystatechange = n.onload = function() {
            !k && g(n.readyState) && (k = 1,
            c(),
            n.onload = n.onreadystatechange = null )
        }
        ,
        o(function() {
            k || (k = 1,
            c(1))
        }, e),
        i ? n.onload() : p.parentNode.insertBefore(n, p)
    }
    ,
    a.yepnope.injectCss = function(a, c, d, e, g, i) {
        var j, e = b.createElement("link"), c = i ? h : c || f;
        e.href = a,
        e.rel = "stylesheet",
        e.type = "text/css";
        for (j in d)
            e.setAttribute(j, d[j]);
        g || (p.parentNode.insertBefore(e, p),
        o(c, 0))
    }
}(this, document),
Modernizr.load = function() {
    yepnope.apply(window, [].slice.call(arguments, 0))
};

var CONFIG = CONFIG || {};
!function() {
	var a = CONFIG;
	a.currentW,
	a.currentH
}(CONFIG);

var INTRO = {
    intropaper: null ,
    init: function() {
        this.intropaper = new Raphael("animintro",CONFIG.currentW,CONFIG.currentH),
        $("#animintro").css({
            width: CONFIG.currentW,
            height: CONFIG.currentH
        }),
        $("#animintro").show();
        var a = INTRO.intropaper.path("M 0," + CONFIG.currentH + " L 0," + CONFIG.currentH).attr({
            fill: "#111"
        })
          , b = INTRO.intropaper.circle(CONFIG.currentW / 2, CONFIG.currentH / 2, 0).attr({
            fill: "#111",
            stroke: "#111"
        })
          , c = INTRO.intropaper.circle(CONFIG.currentW / 2, CONFIG.currentH / 2, 0).attr({
            fill: "#e7e7e7",
            stroke: "none"
        });
        a.animate({
            path: "M 0," + CONFIG.currentH + " L " + CONFIG.currentW / 2 + "," + CONFIG.currentH / 2
        }, 1e3, ">", function() {
            function d() {
                var a = b.clone().attr({
                    r: radiusintro,
                    opacity: 0,
                    fill: "none",
                    "stroke-width": 1
                });
                a.attr({
                    opacity: 1
                }).animate({
                    r: radiusintro + 10,
                    "stroke-width": 41
                }, 1e3, ">", function() {}),
                radiusintro += 40,
                radiusintro < CONFIG.currentW / 2 + 150 ? setTimeout(function() {
                    d()
                }, 50) : setTimeout(function() {
                    function a() {
                        var c = b.clone().attr({
                            transform: "r" + rot + ",151,156t0,-10"
                        });
                        c.animate({
                            transform: "r" + rot + ",151,156t0,0s0.9",
                            opacity: 1
                        }, 2e3, "elastic", function() {}),
                        350 > rot ? (setTimeout(function() {
                            a()
                        }, 20),
                        rot += 10) : ($("body").css("background", "#111"),
                        INTRO.intropaper.clear())
                    }
                    logodeco = new Raphael("logodeco",390,390);
                    var b = logodeco.path("M 149,23 L 153,23 L 151,30 z").attr({
                        fill: "#ccc",
                        opacity: 0,
                        stroke: "none"
                    });
                    a(),
                    $("#logo h1").fadeIn(1500, function() {
                        $("#share").fadeIn(1e3, function() {}),
                        $("#aboutmenu").fadeIn(1e3, function() {}),
                        setTimeout(function() {
                            VL.start()
                        }, 1e3)
                    })
                }, 1400)
            }
            INTRO.arcouno(),
            a.animate({
                path: "M " + CONFIG.currentW / 2 + "," + CONFIG.currentH / 2 + "L " + CONFIG.currentW / 2 + "," + CONFIG.currentH / 2
            }, 1e3, ">", function() {}),
            b.animate({
                r: "121"
            }, 1800, ">", function() {}),
            setTimeout(function() {
                c.animate({
                    r: "140"
                }, 1200, ">", function() {})
            }, 800);
            var e = b.clone();
            setTimeout(function() {
                e.animate({
                    r: "142"
                }, 1200, ">", function() {})
            }, 1100),
            setTimeout(function() {
                d()
            }, 1100)
        })
    },
    arcouno: function() {
        var a = INTRO.intropaper.path(INTRO.arc([CONFIG.currentW / 2, CONFIG.currentH / 2], 120, 120, 320)).attr({
            stroke: "#111",
            "stroke-width": 1
        });
        a.attr({
            transform: "r0," + CONFIG.currentW / 2 + "," + CONFIG.currentH / 2
        }).animate({
            transform: "r360," + CONFIG.currentW / 2 + "," + CONFIG.currentH / 2
        }, 4e3)
    },
    arc: function(a, b, c, d) {
        for (angle = c,
        coords = INTRO.toCoords(a, b, angle),
        path = "M " + coords[0] + " " + coords[1]; d >= angle; )
            coords = INTRO.toCoords(a, b, angle),
            path += " L " + coords[0] + " " + coords[1],
            angle += 1;
        return path
    },
    toCoords: function(a, b, c) {
        var d = c / 180 * Math.PI
          , e = a[0] + Math.cos(d) * b
          , f = a[1] + Math.sin(d) * b;
        return [e, f]
    }
};
Raphael.fn.vlogitWheel = function(a, b, c) {
    function d() {
        if (t == loadingTotal)
            if (g(c, q),
            VL.urldbAll.push(q),
            2 == c) {
                VL.logo3 = m.circle(808, 506, 347).attr({
                    fill: "url(http://vlog.it/img/340.png)",
                    stroke: "none"
                }).attr({
                    opacity: 0
                }),
                imgAll = q;
                {
                    m.vlogitWheel(340, VL.sourceArr.slice(15, 31), 1)
                }
            } else if (1 == c) {
                VL.logo2 = m.circle(807, 507, 247).attr({
                    fill: "url(http://vlog.it/img/240.png)",
                    stroke: "none"
                }).attr({
                    opacity: 0
                }),
                imgAll = imgAll.concat(q);
                {
                    m.vlogitWheel(240, VL.sourceArr.slice(0, 14), 0)
                }
            } else
                0 == c && (imgAll = imgAll.concat(q),
                i());
        else
            setTimeout(d, 50)
    }
    function e() {
        var a = this.title
          , b = this.author
          , c = m.image(this.src, 690, v, 210, 150).attr({
            opacity: 0
        }).click(function() {
            l(),
            VL.slicetoVideoInit(c.node.id, a, b)
        });
        c.node.id = this.id,
        q.push(c),
        t++
    }
    function f() {
        var a = (this.title,
        this.author,
        m.image("http://vlog.it/img/placeholder.jpg", 690, v, 210, 150).attr({
            opacity: 0
        }));
        a.node.id = this.id,
        q.push(a),
        t++
    }
    function g(a, b) {
        slicepath = [m.path("M,800,500,L,853.4050241495155,266.01730107636234,A,240,240,0,0,0,746.5949758504846,266.01730107636234,z").attr({
            stroke: "none"
        }), m.path("M,800,500,L,875.6571175451469,168.52450985817995,A,340,340,0,0,0,724.3428824548531,168.52450985817995,z").attr({
            stroke: "none"
        }), m.path("M,800,500,L,876.4051981734494,66.68458867462846,A,440,440,0,0,0,723.5948018265507,66.68458867462846,z").attr({
            stroke: "none"
        })];
        var c = document.createElementNS(r, "clipPath");
        c.setAttribute("id", "clip_ring" + a),
        s.appendChild(c);
        for (var d = 0; d < b.length; d++) {
            var e = angleplus * d;
            b[d].rotate(e, 800, 500),
            b[d].node.setAttribute("clip-path", "url(#clip_ring" + a + ")"),
            b[d].node.style.cursor = "pointer",
            b[d].attr({
                opacity: 0
            }),
            b[d].hover(function() {}, function() {}),
            c.appendChild(slicepath[a].node),
            h(b[d], angleplus, d),
            o += angleplus,
            n.push(b[d])
        }
    }
    function h(a, b, d) {
        if (1 == c)
            ;
        else
            ;a.attr({
            transform: "r" + b * d + ",800,500t0,140"
        })
    }
    function i() {
        function a() {
            if (b == c.length) {
                j(),
                VL.logo2.attr({
                    opacity: 1
                });
                for (var e = 0; e < c.length; e++)
                    c[e].animate({
                        transform: "r" + (360 + d * e) + ",800,500"
                    }, 3e4)
            } else
                c[b].attr({
                    opacity: 1
                }).animate({
                    transform: "r" + d * b + ",800,500t0,0"
                }, 100, "<>", function() {
                    b++,
                    a()
                })
        }
        console.log("qua");
        var b = 0
          , c = VL.urldbAll[2]
          , d = 360 / 14;
        a(),
        $("#logo").addClass("active")
    }
    function j() {
        for (var a = VL.urldbAll[1], b = 22.5, c = 0; c < a.length; c++)
            c == a.length - 1 ? a[c].attr({
                opacity: 1
            }).animate({
                transform: "r" + b * c + ",800,500t0,0"
            }, 1e3, "<>", function() {
                k();
                for (var c = 0; c < a.length; c++)
                    a[c].animate({
                        transform: "r" + (-360 + b * c) + ",800,500"
                    }, 3e4)
            }) : a[c].attr({
                opacity: 1
            }).animate({
                transform: "r" + b * c + ",800,500t0,0"
            }, 1e3, "<>", function() {
                VL.logo3.attr({
                    opacity: 1
                })
            })
    }
    function k() {
        for (var a = VL.urldbAll[0], b = 20, c = 0; c < a.length; c++)
            c == a.length - 1 ? a[c].attr({
                opacity: 1
            }).animate({
                transform: "r" + b * c + ",800,500t0,0"
            }, 1e3, "<", function() {
                first = !1,
                $("#bumper").hide();
                for (var c = 0; c < a.length; c++)
                    a[c].animate({
                        transform: "r" + (360 + b * c) + ",800,500"
                    }, 3e4)
            }) : a[c].attr({
                opacity: 1
            }).animate({
                transform: "r" + b * c + ",800,500t0,0"
            }, 1e3, "<", function() {})
    }
    function l() {
        $.each(imgAll, function() {
            this.pause()
        })
    }
    var m = this
      , n = this.set()
      , o = 0
      , p = 0
      , q = []
      , r = "http://www.w3.org/2000/svg"
      , s = (document.getElementsByTagName("defs")[0],
    document.getElementsByTagName("svg")[0])
      , t = 0;
    loadingTotal = b.length;
    for (var u = 0; u < b.length; u++) {
        p = null ,
        angleplus = 360 / b.length;
        var v = 240 - 100 * c
          , w = new Image;
        w.onload = e,
        w.onerror = f,
        w.src = b[u].thumb,
        w.id = b[u].url,
        w.title = b[u].title,
        w.author = b[u].author
    }
    return d(),
    n
}
;
var anim_freccia = function(a, b, c, d) {
    animty = a;
    var e = animtop.path("M 0," + (CONFIG.currentH + 500) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 5) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 500) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 500) + " L 0," + (CONFIG.currentH + 500) + "z").attr({
        fill: "#e7e7e7",
        stroke: "none"
    });
    e.animate({
        path: "M 0," + CONFIG.currentH + " L " + (videoboxpos.left + 296) + "," + videoboxpos.top + " L " + CONFIG.currentW + "," + CONFIG.currentH + " L " + CONFIG.currentW + "," + CONFIG.currentH + " L 0," + CONFIG.currentH + "z"
    }, 1e3, "<>", function() {
        e.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 1) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 1) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 1) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 334) + " L " + videoboxpos.left + "," + (videoboxpos.top + 334) + "z"
        }, 500, ">")
    });
    var f = animtop.path("M 0," + (CONFIG.currentH + 500) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 5) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 500)).attr({
        fill: "none",
        "stroke-width": "5px",
        stroke: "#111"
    });
    f.animate({
        path: "M 0," + CONFIG.currentH + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 2) + " L " + CONFIG.currentW + "," + CONFIG.currentH
    }, 1e3, "<>", function() {
        f.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 3) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 3) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 3)
        }, 500, ">")
    });
    var g = animtop.path("M 0," + (CONFIG.currentH + 535) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 25) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 535)).attr({
        fill: "none",
        "stroke-width": "10px",
        stroke: "#111"
    });
    g.animate({
        path: "M 0," + (CONFIG.currentH + 5) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 9) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 5)
    }, 1100, "<>", function() {
        g.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 9) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 9) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 9)
        }, 500, ">")
    });
    var h = animtop.path("M 0," + (CONFIG.currentH + 560) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 50) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 560)).attr({
        fill: "none",
        "stroke-width": "15px",
        stroke: "#111"
    });
    h.animate({
        path: "M 0," + (CONFIG.currentH + 5) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 21) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 15)
    }, 1200, "<>", function() {
        h.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 21) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 21) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 21)
        }, 500, ">")
    });
    var i = animtop.path("M 0," + (CONFIG.currentH + 590) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 75) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 590)).attr({
        fill: "none",
        "stroke-width": "20px",
        stroke: "#111"
    });
    i.animate({
        path: "M 0," + (CONFIG.currentH + 25) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 38) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 35)
    }, 1300, "<>", function() {
        i.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 38) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 38) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 38)
        }, 500, ">")
    });
    var j = animtop.path("M 0," + (CONFIG.currentH + 600) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 90) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 600)).attr({
        fill: "none",
        "stroke-width": "25px",
        stroke: "#111"
    });
    j.animate({
        path: "M 0," + (CONFIG.currentH + 25) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 60) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 35)
    }, 1400, "<>", function() {
        j.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 60) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 60) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 60)
        }, 500, ">")
    });
    var k = animtop.path("M 0," + (CONFIG.currentH + 600) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 115) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 600)).attr({
        fill: "none",
        "stroke-width": "30px",
        stroke: "#111"
    });
    k.animate({
        path: "M 0," + (CONFIG.currentH + 25) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 87) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 35)
    }, 1500, "<>", function() {
        k.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 87) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 87) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 87)
        }, 500, ">")
    });
    var l = animtop.path("M 0," + (CONFIG.currentH + 600) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 115) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 600)).attr({
        fill: "none",
        "stroke-width": "35px",
        stroke: "#111"
    });
    l.animate({
        path: "M 0," + (CONFIG.currentH + 25) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 119) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 35)
    }, 1600, "<>", function() {
        l.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 119) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 119) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 119)
        }, 500, ">")
    });
    var m = animtop.path("M 0," + (CONFIG.currentH + 600) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 115) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 600)).attr({
        fill: "none",
        "stroke-width": "40px",
        stroke: "#111"
    });
    m.animate({
        path: "M 0," + (CONFIG.currentH + 25) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 156) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 35)
    }, 1700, "<>", function() {
        m.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 156) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 156) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 156)
        }, 500, ">")
    });
    var n = animtop.path("M 0," + (CONFIG.currentH + 600) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 115) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 600)).attr({
        fill: "none",
        "stroke-width": "45px",
        stroke: "#111"
    });
    n.animate({
        path: "M 0," + (CONFIG.currentH + 25) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 198) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 35)
    }, 1800, "<>", function() {
        n.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 198) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 198) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 198)
        }, 500, ">")
    });
    var o = animtop.path("M 0," + (CONFIG.currentH + 600) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 115) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 600)).attr({
        fill: "none",
        "stroke-width": "50px",
        stroke: "#111"
    });
    o.animate({
        path: "M 0," + (CONFIG.currentH + 25) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 245) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 35)
    }, 1900, "<>", function() {
        o.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 245) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 245) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 245)
        }, 500, ">")
    });
    var p = animtop.path("M 0," + (CONFIG.currentH + 600) + " L " + (videoboxpos.left + 296) + "," + (CONFIG.currentH + 115) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 600)).attr({
        fill: "none",
        "stroke-width": "64px",
        stroke: "#111"
    });
    p.animate({
        path: "M 0," + (CONFIG.currentH + 25) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 301) + " L " + CONFIG.currentW + "," + (CONFIG.currentH + 35)
    }, 2e3, "<>", function() {
        p.animate({
            path: "M " + videoboxpos.left + "," + (videoboxpos.top + 302) + " L " + (videoboxpos.left + 296) + "," + (videoboxpos.top + 302) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 302)
        }, 500, ">", function() {
            0 == animty ? VL.slicetoVideo(b, c, d) : openAbout(),
            $("#close").show(),
            sxcross.animate({
                path: "M 10,10 L 30,30"
            }, 500, ">", function() {
                dxcross.animate({
                    path: "M 10,30 L 30,10"
                }, 500, ">")
            })
        })
    })
}
  , anim_puzzle = function(a, b, c, d) {
    animty = a;
    var e = animtop.path("M -200," + videoboxpos.top + " L -200," + videoboxpos.top + " L -200," + (videoboxpos.top + 167) + " L -200," + (videoboxpos.top + 334) + " L -200," + (videoboxpos.top + 334) + "z").attr({
        fill: "#111",
        stroke: "none"
    });
    e.animate({
        path: "M -200," + videoboxpos.top + " L " + (videoboxpos.left + 150) + "," + videoboxpos.top + " L " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 167) + " L " + (videoboxpos.left + 300) + "," + (videoboxpos.top + 334) + " L -200," + (videoboxpos.top + 334) + "z"
    }, 1e3, "<>", function() {
        e.animate({
            path: "M " + videoboxpos.left + "," + videoboxpos.top + " L " + (videoboxpos.left + 150) + "," + videoboxpos.top + " L " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 167) + " L " + (videoboxpos.left + 300) + "," + (videoboxpos.top + 334) + " L " + videoboxpos.left + "," + (videoboxpos.top + 334) + "z"
        }, 500, ">", function() {})
    });
    var f = animtop.path("M " + (videoboxpos.left + 150) + ",-200 L " + (videoboxpos.left + 592) + ",-200 L " + (videoboxpos.left + 592) + ",-200 L " + (videoboxpos.left + 400) + ",-200 L " + (videoboxpos.left + 150) + ",-200z").attr({
        fill: "#e7e7e7",
        stroke: "none"
    });
    f.animate({
        path: "M " + (videoboxpos.left + 150) + ",0 L " + (videoboxpos.left + 592) + ",0 L " + (videoboxpos.left + 592) + "," + videoboxpos.top + " L " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 167) + " L " + (videoboxpos.left + 150) + "," + videoboxpos.top + "z"
    }, 1e3, "<>", function() {
        f.animate({
            path: "M " + (videoboxpos.left + 150) + "," + videoboxpos.top + " L " + (videoboxpos.left + 592) + "," + videoboxpos.top + " L " + (videoboxpos.left + 592) + "," + videoboxpos.top + " L " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 167) + " L " + (videoboxpos.left + 150) + "," + videoboxpos.top + "z"
        }, 500, "<>", function() {})
    });
    var g = animtop.path("M " + (CONFIG.currentW + 200) + "," + videoboxpos.top + " L " + (CONFIG.currentW + 200) + "," + (videoboxpos.top + 334) + " L " + (CONFIG.currentW + 200) + "," + (videoboxpos.top + 334) + " L " + (CONFIG.currentW + 200) + "," + (videoboxpos.top + 167) + " L " + (CONFIG.currentW + 200) + "," + videoboxpos.top + "z").attr({
        fill: "#111",
        stroke: "none"
    });
    g.animate({
        path: "M " + (CONFIG.currentW + 200) + "," + videoboxpos.top + " L " + (CONFIG.currentW + 200) + "," + (videoboxpos.top + 334) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 334) + " L " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 167) + " L " + (videoboxpos.left + 592) + "," + videoboxpos.top + "z"
    }, 1e3, "<>", function() {
        g.animate({
            path: "M " + (videoboxpos.left + 592) + "," + videoboxpos.top + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 334) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 334) + " L " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 167) + " L " + (videoboxpos.left + 592) + "," + videoboxpos.top + "z"
        }, 500, "<>", function() {})
    });
    var h = animtop.path("M " + (videoboxpos.left + 400) + "," + (CONFIG.currentH + 200) + " L " + (videoboxpos.left + 592) + "," + (CONFIG.currentH + 200) + " L " + (videoboxpos.left + 592) + "," + (CONFIG.currentH + 200) + " L " + (videoboxpos.left + 300) + "," + (CONFIG.currentH + 200) + " L " + (videoboxpos.left + 300) + "," + (CONFIG.currentH + 200) + "z").attr({
        fill: "#e7e7e7",
        stroke: "none"
    });
    h.animate({
        path: "M " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 167) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 334) + " L " + (videoboxpos.left + 592) + "," + (CONFIG.currentH + 200) + " L " + (videoboxpos.left + 300) + "," + (CONFIG.currentH + 200) + " L " + (videoboxpos.left + 300) + "," + (videoboxpos.top + 334) + "z"
    }, 1e3, "<>", function() {
        h.animate({
            path: "M " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 167) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 334) + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 334) + " L " + (videoboxpos.left + 300) + "," + (videoboxpos.top + 334) + " L " + (videoboxpos.left + 300) + "," + (videoboxpos.top + 334) + "z"
        }, 500, "<>", function() {
            setTimeout(function() {
                e.toFront(),
                g.toFront(),
                e.animate({
                    path: "M " + videoboxpos.left + "," + videoboxpos.top + " L " + (videoboxpos.left + 400) + "," + videoboxpos.top + " L " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 167) + " L " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 334) + " L " + videoboxpos.left + "," + (videoboxpos.top + 334) + "z"
                }, 500, ">", function() {}),
                g.animate({
                    path: "M " + (videoboxpos.left + 592) + "," + videoboxpos.top + " L " + (videoboxpos.left + 592) + "," + (videoboxpos.top + 334) + " L " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 334) + " L " + (videoboxpos.left + 400) + "," + (videoboxpos.top + 167) + " L " + (videoboxpos.left + 400) + "," + videoboxpos.top + "z"
                }, 500, "<>", function() {
                    0 == animty ? VL.slicetoVideo(b, c, d) : openAbout(),
                    $("#close").show(),
                    sxcross.animate({
                        path: "M 10,10 L 30,30"
                    }, 500, ">", function() {
                        dxcross.animate({
                            path: "M 10,30 L 30,10"
                        }, 500, ">")
                    })
                })
            }, 500)
        })
    })
}
  , anim_zebra = function(a, b, c, d) {
    function e() {
        var a = g.clone().attr({
            transform: "t" + j + ",0"
        });
        a.animate({
            path: "M " + (videoboxpos.left + 9.2) + ",-200  L " + (videoboxpos.left + 9.2) + "," + (videoboxpos.top + 334)
        }, 150, ">", function() {
            function f() {
                g < k.length ? (k[g].toFront(),
                k[g].animate({
                    "stroke-width": "50px",
                    transform: "t" + (36.8 * g + 14) + ",0"
                }, 50, "<>", f),
                g++) : (l[15].attr({
                    stroke: "#111",
                    "stroke-width": "21px"
                }),
                0 == animty ? VL.slicetoVideo(b, c, d) : openAbout(),
                $("#close").show(),
                sxcross.animate({
                    path: "M 10,10 L 30,30"
                }, 500, ">", function() {
                    dxcross.animate({
                        path: "M 10,30 L 30,10"
                    }, 500, ">")
                }))
            }
            if (a.animate({
                path: "M " + (videoboxpos.left + 9.2) + "," + videoboxpos.top + " L " + (videoboxpos.left + 9.2) + "," + (videoboxpos.top + 334)
            }, 150, ">", function() {}),
            522 > j)
                k.push(a),
                setTimeout(function() {
                    e()
                }, 10),
                j += 36.8;
            else {
                var g = 0;
                f()
            }
        })
    }
    function f() {
        var a = h.clone().attr({
            transform: "t" + (19.2 + i) + ",0"
        });
        l.push(a),
        a.animate({
            path: "M " + (videoboxpos.left + 9.2) + "," + videoboxpos.top + " L " + (videoboxpos.left + 9.2) + "," + (CONFIG.currentH + 20)
        }, 150, ">", function() {
            a.animate({
                path: "M " + (videoboxpos.left + 9.2) + "," + videoboxpos.top + " L " + (videoboxpos.left + 9.2) + "," + (videoboxpos.top + 334)
            }, 150, ">", function() {}),
            522 > i && (setTimeout(function() {
                f()
            }, 10),
            i += 36.8)
        })
    }
    animty = a;
    var g = animtop.path("M " + (videoboxpos.left + 9.2) + ",-200 L " + (videoboxpos.left + 9.2) + ",-200").attr({
        fill: "none",
        "stroke-width": "18.4px",
        stroke: "#111"
    })
      , h = animtop.path("M " + (videoboxpos.left + 9.2) + "," + (CONFIG.currentH + 200) + " L " + (videoboxpos.left + 9.2) + "," + (CONFIG.currentH + 200)).attr({
        fill: "none",
        "stroke-width": "18.4px",
        stroke: "#e7e7e7"
    })
      , i = 0
      , j = 0
      , k = []
      , l = [];
    e(),
    f()
}
  , vlogitBoxer = [anim_freccia, anim_puzzle, anim_zebra]
  , VL = VL || {};
VL.window = window;
var VL = {
    first: !0,
    paper: null ,
    sourceArr: [],
    imgAll: [],
    urldbAll: [],
    logo2: null ,
    logo3: null ,
    init: function() {
        INTRO.init(),
        $("#animtop").css({
            width: CONFIG.currentW,
            height: CONFIG.currentH
        }),
        animtop = new Raphael("animtop",CONFIG.currentW,CONFIG.currentH);
        var a = new Raphael("closesvg",40,40)
          , b = a.path(INTRO.arc([20, 20], 10, 0, 360));
        b.attr({
            stroke: "#111",
            "stroke-width": 20
        }),
        b.rotate(180, 20, 20),
        sxcross = a.path("M 10,10 L 10,10").attr({
            stroke: "#e7e7e7",
            "stroke-width": 2
        }).toFront(),
        dxcross = a.path("M 10,30 L 10,30").attr({
            stroke: "#e7e7e7",
            "stroke-width": 2
        }).toFront()
    },
    evHandlers: function() {
        $("#listasoc").css("opacity", .8),
        $("#listasoc").hover(function() {
            $(this).animate({
                opacity: 1
            }, 500)
        }, function() {
            $(this).animate({
                opacity: .7
            }, 500),
            $("#share").show(),
            $("#listasoc").hide(),
            $("#aboutmenu").show()
        }),
        $("#share").hover(function() {
            $("#listasoc").show(),
            $("#share").hide(),
            $("#aboutmenu").hide()
        }, function() {}),
        $("#descr").hover(function() {
            $("#descr h3").animate({
                left: 0
            }, 2e3, function() {
                $(this).delay(4e3).animate({
                    left: -400
                }, 2e3)
            }),
            $("#descr h4").animate({
                left: 0
            }, 2e3, function() {
                $(this).delay(3500).animate({
                    left: -400
                }, 2e3)
            })
        }, function() {}),
        $("#close b, #overlay").click(function(a) {
            return a.preventDefault(),
            $("#overlay").fadeOut(500),
            $("#vimejo").hide(),
            $("#videobox").css("background", "transparent"),
            $("#videobox").hide(),
            $("#vimejo").html(""),
            $("#descr h3 span").html(""),
            $("#descr h4 span").html(""),
            $("#descr h3").css("left", "-420px"),
            $("#descr h4").css("left", "-420px"),
            $("#descr").hide(),
            $("#close").hide(),
            $("#about h2").hide(),
            $("#about #about_content").hide(),
            animtop.clear(),
            $.each(VL.imgAll, function() {
                this.resume()
            }),
            !1
        }),
        $("#aboutmenu").click(function(a) {
            return a.preventDefault(),
            $("#videobox").show(),
            videoboxpos = $("#videobox").offset(),
            $("#animtop").show(),
            estratto = vlogitBoxer[Math.floor(3 * Math.random())],
            estratto(1),
            !1
        })
    },
    start: function() {
        1 == this.first ? (this.paper = new Raphael("vlogitwheel",2e3,1e3),
        this.sourceArr = vmvideos) : (response = "",
        this.paper.clear(),
        this.sourceArr = [],
        this.imgAll = [],
        this.urldbAll = [],
        this.sourceArr = vmvideos),
        $("#bumper").css({
            display: "block",
            height: CONFIG.currentH
        });
        this.paper.vlogitWheel(440, VL.sourceArr.slice(32, 51), 2)
    },
    slicetoVideoInit: function(a, b, c) {
        $("#videobox").show(),
        $("#overlay").fadeIn(500),
        videoboxpos = $("#videobox").offset(),
        $("#animtop").show(),
        (estratto = vlogitBoxer[Math.floor(3 * Math.random())])(0, a, b, c)
    },
    slicetoVideo: function(a, b, c) {
        $("#descr").show(),
        $("#animtop").hide(),
        b.length > 40 && (b = b.substring(0, 33) + "..."),
        c = "by " + c,
        $("#descr h3 span").html(b.toLowerCase()),
        $("#descr h4 span").html(c.toLowerCase()),
        $("#close").show(),
        $("#vimejo").show();
        var d = a;
        $.getScript(oEmbedUrl + "?url=" + encodeURIComponent(d) + "&width=592&height=334&autoplay=true&loop=true&wmode=opaque&color=e7e7e7&callback=embedVideo"),
        animtop.clear()
    },
    resize: function() {
        CONFIG.currentW = $(window).width(),
        CONFIG.currentH = $(window).height()
    }
};
$(function() {
    VL.resize(),
    VL.init(),
    VL.evHandlers(),
    $(window).resize(function() {
        VL.resize()
    })
});
var animtop, logodeco, videoboxpos, angleone, angletwo, anglethree, sxcross, dxcross, vimeo, youp, anim3, estratto, animty,
	response = "",
	radiusintro = 140,
	rot = 0,
	oEmbedUrl = "http://vimeo.com/api/oembed.json";
