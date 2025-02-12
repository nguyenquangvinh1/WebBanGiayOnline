const myPolarAreaChart = new Chart(document.getElementById('myPolarAreaChart'), {
    type: 'polarArea',
    data: {
        labels: [
            'Áo sơ mi HenTile',
            'Quần âu QC',
            'Vest PL',
            'Áo thể thao Polo',
            'Tất'
        ],
        datasets: [{
            label: 'My First Dataset',
            data: [112, 33, 70, 399, 164],
            backgroundColor: [
                'rgb(255, 99, 132)',
                'rgb(75, 192, 192)',
                'rgb(255, 205, 86)',
                'rgb(201, 203, 207)',
                'rgb(54, 162, 235)'
            ]
        }]
    },
    options: {}
});

const pieChart = new Chart(document.getElementById('pieChart'), {
    type: 'pie',
    data: {
        labels: [
            'Red',
            'Blue',
            'Yellow'
        ],
        datasets: [{
            label: 'My First Dataset',
            data: [300, 50, 100],
            backgroundColor: [
                'rgb(255, 99, 132)',
                'rgb(54, 162, 235)',
                'rgb(255, 205, 86)'
            ],
            hoverOffset: 4
        }]
    },
    options: {}
});

// Tạo danh sách tháng
const labels = ["T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12"];
const LineChart = new Chart(document.getElementById('LineChart'), {
    type: 'line',
    data: {
        labels: labels,
        datasets: [{
            label: 'My First Dataset',
            data: [65, 59, 80, 81, 56, 55, 40, 88, 50, 55,],
            fill: false,
            borderColor: 'rgb(75, 192, 192)',
            tension: 0.1
        }]
    },
});




