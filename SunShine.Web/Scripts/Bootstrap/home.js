
jQuery(document).ready(function () {

    jQuery(".hd").click(function () {
        alert("aaa");
    })

    

    if (jQuery.query.get("kw") != "") {
        if (jQuery("#Keywords").hasClass("keywordB")) {
            jQuery("#Keywords").removeClass("keywordB");
        }
        jQuery("#Keywords").val(jQuery.query.get("kw"));
    }
    jQuery("#Keywords").focus(function () {
        if (jQuery("#Keywords").hasClass("keywordB")) {
            jQuery("#Keywords").removeClass("keywordB");
            jQuery("#Keywords").keyup();
        }
    });
    jQuery("#Keywords").blur(function () {
        if (jQuery("#Keywords").val() != "") {
            if (jQuery("#Keywords").hasClass("keywordB")) {
                jQuery("#Keywords").removeClass("keywordB");
            }
        }
        else {
            if (!jQuery("#Keywords").hasClass("keywordB")) {
                jQuery("#Keywords").addClass("keywordB");
            }
        }
    });


    //结果搜索文本框
    jQuery("#ResultKW").focus(function () {
        if (jQuery("#ResultKW").hasClass("keywordB")) {
            jQuery("#ResultKW").removeClass("keywordB");
        }
    });
    jQuery("#ResultKW").blur(function () {
        if (jQuery("#ResultKW").val() != "") {
            if (jQuery("#ResultKW").hasClass("keywordB")) {
                jQuery("#ResultKW").removeClass("keywordB");
            }
        }
        else {
            if (!jQuery("#ResultKW").hasClass("keywordB")) {
                jQuery("#ResultKW").addClass("keywordB");
            }
        }
    });

    jQuery("#logmail").focus(function () {
        if (jQuery("#logmail").hasClass("email")) {
            jQuery("#logmail").removeClass("email");
        }
    });
    jQuery("#logmail").blur(function () {
        if (jQuery("#logmail").val() != "") {
            if (jQuery("#logmail").hasClass("email")) {
                2
                jQuery("#logmail").removeClass("email");
            }
        }
        else {
            if (!jQuery("#logmail").hasClass("email")) {
                jQuery("#logmail").addClass("email");
            }
        }
    });
    jQuery("#logpwd").focus(function () {
        if (jQuery("#logpwd").hasClass("password")) {
            jQuery("#logpwd").removeClass("password");
        }
    });
    jQuery("#logpwd").blur(function () {
        if (jQuery("#logpwd").val() != "") {
            if (jQuery("#logpwd").hasClass("password")) {
                jQuery("#logpwd").removeClass("password");
            }
        }
        else {
            if (!jQuery("#logpwd").hasClass("password")) {
                jQuery("#logpwd").addClass("password");
            }
        }
    });

    jQuery(".listCol> ul >div>li").hover(function () {
        var parentLi = jQuery(this);
        jQuery(this).addClass("hover");
        if (jQuery(this).attr("hasChildren") == "true") {
            return false;
        }
        else {
            if (jQuery(this).find(".thirdLevel").length > 0) {
                jQuery(this).find(".thirdLevel").show();
            }
            else {

                var url = "/GetPhoneStyleChildren.aspx?psid=" + jQuery(this).attr("key") + "&index=" + jQuery(this).attr("index");
                jQuery.get(url, null, function (redata) {
                    jQuery(parentLi).append(redata);
                    jQuery(parentLi).find(".thirdLevel >li").hover(function () {
                        jQuery(this).addClass("thirdHover");
                    }, function () {
                        jQuery(this).removeClass("thirdHover");
                    });
                    jQuery(this).find(".thirdLevel").show();
                });
            }
        }

    }, function () {
        jQuery(this).removeClass("hover");
        jQuery(this).find(".thirdLevel").hide();
    });

    var urlArr = new Array();
    urlArr.push("/product/NewArrival.aspx");
    urlArr.push("/product/comingsoon.aspx");
    urlArr.push("/main/Comments.aspx|/service/service.aspx");
    urlArr.push("/UPSTrack.aspx");
    urlArr.push("/main/company.aspx|/legal/legal.aspx|/careers.aspx");
    urlArr.push("/product/Special.aspx");
    var aIdArr = new Array();
    aIdArr.push("#aNewArrival");
    aIdArr.push("#acomingsoon");
    aIdArr.push("#aservice");
    aIdArr.push("#aups");
    aIdArr.push("#acompany");
    aIdArr.push("#clearance");
    var j = 0;
    for (var i = 0; i < urlArr.length; i++) {
        var urlArrSame = urlArr[i].split("|");
        for (var k = 0; k < urlArrSame.length; k++) {
            if (window.location.href.toLowerCase().indexOf(urlArrSame[k].toLowerCase()) >= 0) {
                jQuery("#nav > ul>li>a").each(function () {
                    jQuery(aIdArr[i]).addClass("currentItem");
                    j++;
                });
            }
        }

    }
    if (j == 0) {
        jQuery("#ahome").addClass("currentItem");
    }
    if (jQuery.query.get("IsPromotion") == "1") {
        jQuery("#chkClearance").attr("checked", true);
    }
    if (window.location.href.indexOf("productquicklyorder.aspx") >= 0) {
        jQuery("#chkQuickly").attr("checked", true);
    }
    jQuery.get("/GetPageSize.aspx", { randNum: getDateNum() }, function (redata) {
        jQuery("#pagesize").val(redata);
    });

    jQuery("#pagesize").change(function () {
        jQuery.post("/AddSession.aspx", { ckname: "pagesize", ckval: jQuery("#pagesize").val() }, function () {
            window.location.href = window.location.href;
        });
    });

    // Accordion
    // jQuery('#menu5 > li > a.expanded + ul').slideToggle('medium');
    var i = 0;
    jQuery('#menu5 > li > a').click(function () {
        jQuery('#menu5 > li > a.expanded').not(this).toggleClass('expanded').toggleClass('collapsed').parent().find('> ul').slideToggle('medium');
        jQuery(this).toggleClass('expanded').toggleClass('collapsed').parent().find('> ul').slideToggle('medium');

        var evt = this;
        jQuery(evt).children().first().removeClass();
        if (jQuery(evt).children().first().attr("src").indexOf("expand0.gif") >= 0) {
            jQuery(evt).children().first().attr("src", "/images/fold0.gif");
        }
        else {
            jQuery(evt).children().first().attr("src", "/images/expand0.gif");
        }

        jQuery(evt).children().first().removeClass()
        jQuery(evt).children().first().addClass("expandedImg1");
        jQuery('.expandedImg').each(function () {
            jQuery(this).attr("src", "/images/fold0.gif");
        });
        jQuery(evt).children().first().removeClass()
        jQuery(evt).children().first().addClass("expandedImg");

        i++;
    });

    function nextKeyword(next) {
        var index = -1;
        jQuery("#SmartTips li").each(function (i, d) {
            if (jQuery(d).hasClass("hover")) {
                index = i;
                keywordMouseOut(jQuery(d));

            }
        });
        if (next == 1) {
            if (index == jQuery("#SmartTips li").length - 1) {
                index = -1;
            }
        }
        else if (next == -1) {
            if (index == 0) {
                index == jQuery("#SmartTips li").length - 1;
            }
            else if (index == -1) {
                index = 1;
            }
        }

        index = index + next;

        keywordMouseIn(jQuery("#SmartTips li").eq(index));
    }

    function keywordMouseIn(obj) {
        if (!jQuery(obj).hasClass("hover")) {
            jQuery(obj).addClass("hover");
            if (jQuery(obj).html() != "Close") {
                jQuery("#Keywords").val(jQuery(obj).text());
            }

        }
    }

    function keywordMouseOut(obj) {
        if (jQuery(obj).hasClass("hover")) {
            jQuery(obj).removeClass("hover");
        }
    }

    var ajaxResult = null;
    var timer1;
    jQuery("#Keywords").keyup(function (event) {
        if (event.which == 40) {
            nextKeyword(1);
            return;
        }
        else if (event.which == 38) {
            nextKeyword(-1);
            return;
        }
        //        jQuery(".SmartTips").hide();
        //        jQuery("#SmartTips li").children().remove();
        var url = "/Product/SearchSmartTips.aspx";
        var data = { keyword: jQuery(this).val(), randNum: getDateNum() };
        if (ajaxResult != null) {
            ajaxResult.abort();
        }

        clearInterval(timer1);
        timer1 = setTimeout(function () {
            ajaxResult = jQuery.get(url, data, function (redata) {
                jQuery("#SmartTips").children().remove();
                if (redata == "") {
                    return;
                }
                redata = eval("(" + redata + ")");
                if (redata.length > 0) {
                    jQuery(".SmartTips").show();
                }
                for (var i = 0; i < redata.length; i++) {
                    var keyword = redata[i].Keyword;
                    keyword = keyword.replace(/'/g, '\\\'');
                    keyword = keyword.replace(/"/g, '\\"');
                    var linkpath = redata[i].Path;
                    linkpath = "/Oservice/smallimages/" + linkpath;
                    if (i % 2 == 0) {
                        jQuery("#SmartTips").append("<li onclick=\"getKeyword('" + keyword + "'," + redata[i].Num + "," + redata[i].Pid + ");\" class='colorstyletest2' >" + "<img src='" + linkpath + "' height='51px' width='51px' />" + "<p>" + redata[i].Keyword + "</p>" + "</li>");
                    }
                    else {
                        jQuery("#SmartTips").append("<li onclick=\"getKeyword('" + keyword + "'," + redata[i].Num + "," + redata[i].Pid + ");\" class='colorstyletest' >" + "<img src='" + linkpath + "' height='51px' width='51px' />" + "<p>" + redata[i].Keyword + "</p>" + "</li>");
                    }
                }
                jQuery("#SmartTips").append("<li style=\"text-align:right;height:25px;\"  onclick=\"closeSmartTips()\">Close</li>");

                jQuery("#SmartTips li").mouseover(function () {
                    keywordMouseIn(jQuery(this));
                });
                jQuery("#SmartTips li").mouseout(function () {
                    keywordMouseOut(jQuery(this));
                });

            });
        }, 500);
    });
    setTimeout(function () {
        if (i == 0)
            jQuery('#aBra').click();
    }, 250);
    jQuery("#btnSearch").click(function () {
        SearchProducts();
    });
    jQuery(".SmartTips").hover(function () { }, function () {
        jQuery(".SmartTips").hide();
    });
});
function SearchProducts() {
    var isPromotion = "-1";
    if (jQuery("#chkClearance").attr("checked")) {
        isPromotion = "1";
    }
    var keyword = Trim(jQuery("#Keywords").val());
    if (Trim(keyword) != "") {
        window.location.href = "/product/productsearch.aspx?kw=" + escape(keyword) + "&IsPromotion=" + isPromotion;

    }
}

function closeSmartTips() {
    jQuery(".SmartTips").hide();
}
function getKeyword(keyword, num, pid) {
    keyword = Trim(keyword);
    var isPromotion = "-1";
    if (jQuery("#chkClearance").attr("checked")) {
        isPromotion = "1";
    }
    if (Trim(keyword) != "") {
        window.location.href = "/product/productsearch.aspx?kw=" + escape(keyword) + "&IsPromotion=" + isPromotion;

    }
}

function imgError(img) {
    jQuery(img).attr("src", "/webpic/model/0001.jpg");
}

var obje;
var str = "";
function picOver(id) {
    obje = "#divcpm" + id;
    if (str.indexOf(id + ",") >= 0) {
        jQuery("#divcpm" + id).show();
    }
    else {
        jQuery("#divcpm" + id).find("img").attr("src", jQuery("#divcpm" + id).find("img").attr("link"));
        jQuery("#divcpm" + id).find("img").load(
                    function () {
                        if (obje == "#divcpm" + id) {
                            jQuery("#divcpm" + id).show();

                        } str += id + ",";
                    });

    }
}

function picOut(id) {
    jQuery("#divcpm" + id).hide();
    obje = "";
}

jQuery(document).ready(function () {
    jQuery("#nav-one li").hover(
		function () { jQuery("ul", this).fadeIn("fast"); },
		function () { }
	);
    if (document.all) {
        jQuery("#nav-one li").hoverClass("sfHover");
    }
});

jQuery.fn.hoverClass = function (c) {
    return this.each(function () {
        jQuery(this).hover(
			function () { jQuery(this).addClass(c); },
			function () { jQuery(this).removeClass(c); }
		);
    });
};