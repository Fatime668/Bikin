$(".bar a i").click(function(){
    $(".navbar").css("display","block")
})
$(".close i").click(function(){
    $(".navbar").css("display","none")

})
const gotopbtn = document.querySelector(".gotopbtn")
window.addEventListener("scroll",()=>{
    if(window.pageXOffset > 100){
        gotopbtn.classList.add("active");
    }else{
        gotopbtn.classList.remove("active")
    }
})