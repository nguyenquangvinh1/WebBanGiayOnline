
export default class Core {

	constructor() {
		this.loadPerfectScrollbar(() => {
			this.pfScrollBar();
		});
		this.sideNav();
		this.tooltipInit();
		this.popOverInit();
		this.toastInit();
	}

	loadPerfectScrollbar(callback) {
		if (typeof PerfectScrollbar === "undefined") {
			console.warn("⚠️ Perfect Scrollbar chưa được tải! Đang tải từ CDN...");
			let script = document.createElement("script");
			script.src = "https://cdnjs.cloudflare.com/ajax/libs/jquery.perfect-scrollbar/1.5.5/perfect-scrollbar.min.js";
			script.onload = function () {
				console.log("✅ Perfect Scrollbar đã tải xong!");
				if (typeof callback === "function") callback();
			};
			document.head.appendChild(script);
		} else {
			console.log("✅ Perfect Scrollbar đã có sẵn!");
			if (typeof callback === "function") callback();
		}
	}
	
    sideNav() {
		const appLayout =  $('.app');
		const isFolded = 'is-folded';
		const isExpand = 'is-expand';
		const active = 'active';
		const drodpDownItem = '.side-nav .side-nav-menu .nav-item .dropdown-menu li'

		
			if ($(drodpDownItem).hasClass('active')) {
				$( drodpDownItem + '.' + active).parent().parent().addClass('open') 
			}

        $('.side-nav .side-nav-menu li a').on('click', (e) => {
			const $this = $(e.currentTarget);
			
			if ($this.parent().hasClass("open")) {

				$this.parent().children('.dropdown-menu').slideUp(200, ()=> {
					$this.parent().removeClass("open");
				});

			} else {
				$this.parent().parent().children('li.open').children('.dropdown-menu').slideUp(200);
				$this.parent().parent().children('li.open').children('a').removeClass('open');
				$this.parent().parent().children('li.open').removeClass("open");
				$this.parent().children('.dropdown-menu').slideDown(200, ()=> {
					$this.parent().addClass("open");
				});
			}
		});

		$('.header .nav-left .desktop-toggle').on('click', () => {
			appLayout.toggleClass(isFolded)
		});

		$('.header .nav-left .mobile-toggle').on('click', () => {
			appLayout.toggleClass(isExpand)
		});
	} 

	pfScrollBar() {
		document.querySelectorAll('.scrollable').forEach(el => {
			new PerfectScrollbar(el, {
				wheelSpeed: 1,
				wheelPropagation: true,
				minScrollbarLength: 20
			});
		});
		console.log("✅ Perfect Scrollbar đã khởi tạo thành công!");
	}

	
	tooltipInit() {
		$('[data-toggle="tooltip"]').tooltip()
	}

	popOverInit() {
		$('[data-toggle="popover"]').popover({
			trigger: 'focus'
		})
	}

	toastInit() {
		$('.toast').toast();
	}
	console.log("✅ jQuery Version:", $.fn.jquery);
	console.log("✅ Perfect Scrollbar exists:", typeof PerfectScrollbar !== "undefined");

}    