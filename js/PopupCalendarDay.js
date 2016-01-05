var oCalendarChs=new PopupCalendar("oCalendarChs");	//初始化控件时,请给出实例名称:oCalendarChs
oCalendarChs.weekDaySting = new Array("S", "M", "T", "W", "T", "F", "S");
oCalendarChs.monthSting = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
oCalendarChs.oBtnTodayTitle="Today";
oCalendarChs.oBtnCancelTitle="Cancel";
oCalendarChs.oBtnCleanTitle="Clear";
oCalendarChs.Init();


function PopupCalendar(InstanceName)
{
	///Global Tag
	this.instanceName=InstanceName;
	///Properties
	this.separator="-"
	this.oBtnTodayTitle="Today"
	this.oBtnCancelTitle="Cancel"
	this.oBtnCleanTitle="Clean"
	this.weekDaySting=new Array("S","M","T","W","T","F","S");
	this.monthSting=new Array("January","February","March","April","May","June","July","August","September","October","November","December");
	this.Width=200;
	this.currDate=new Date();
	this.today=new Date();
	this.startYear=1908;
	this.endYear=2108;
	///Css
	this.normalfontColor="#666666";
	this.selectedfontColor="red";
	this.divBorderCss="1px solid #BCD0DE";
	this.titleTableBgColor="#98B8CD";
	this.tableBorderColor="#CCCCCC"
	///Method
	this.Init=CalendarInit;
	this.Fill=CalendarFill;
	this.Refresh=CalendarRefresh;
	this.Restore=CalendarRestore;
	///HTMLObject
	this.oTaget=null;
	this.oPreviousCell=null;
	this.sDIVID=InstanceName+"_Div";
	this.sTABLEID=InstanceName+"_Table";
	this.sMONTHID=InstanceName+"_Month";
	this.sYEARID=InstanceName+"_Year";
	this.sTODAYBTNID=InstanceName+"_TODAYBTN";
	this.sCLEANBTNID=InstanceName+"_CLEANBTN";
	
}
function CalendarInit()				///Create panel
{
	var sMonth,sYear
	sMonth=this.currDate.getMonth();
	sYear=this.currDate.getYear();
	htmlAll="<div id='"+this.sDIVID+"' style='display:none;position:absolute;width:200px;border:"+this.divBorderCss+";padding:2px;background-color:#FFFFFF'>";
	htmlAll+="<div align='center'>";
	/// Month
	htmloMonth="<select id='"+this.sMONTHID+"' onchange=CalendarMonthChange("+this.instanceName+") style='width:50%;height:20px;'>";
	for(i=0;i<12;i++)
	{			
		htmloMonth+="<option value='"+i+"'>"+this.monthSting[i]+"</option>";
	}
	htmloMonth+="</select>";
	/// Year
	htmloYear = "<select id='" + this.sYEARID + "' onchange=CalendarYearChange(" + this.instanceName + ") style='width:50%;height:20px;'>";
	for(i=this.startYear;i<=this.endYear;i++)
	{
		htmloYear+="<option value='"+i+"'>"+i+"</option>";
	}
	htmloYear+="</select></div>";
	/// Day
	htmloDayTable="<table id='"+this.sTABLEID+"' width='100%' border=0 cellpadding=0 cellspacing=1 bgcolor='"+this.tableBorderColor+"'>";
	htmloDayTable+="<tbody bgcolor='#ffffff'style='font-size:13px'>";
	for(i=0;i<=6;i++)
	{
		if(i==0)
			htmloDayTable+="<tr bgcolor='" + this.titleTableBgColor + "'>";
		else
			htmloDayTable+="<tr>";
		for(j=0;j<7;j++)
		{

			if(i==0)
			{
				htmloDayTable+="<td height='20' align='center' valign='middle' style='cursor:hand'>";
				htmloDayTable+=this.weekDaySting[j]+"</td>"
			}
			else {
			    if (j == 0 || j == 6) {
			        htmloDayTable += "<td height='20' align='center' valign='middle' style='cursor:hand;'";
			    }
			    else {
			        htmloDayTable += "<td height='20' align='center' valign='middle' style='cursor:hand;'";
			    }
				htmloDayTable+=" onmouseover=CalendarCellsMsOver("+this.instanceName+")";
				htmloDayTable+=" onmouseout=CalendarCellsMsOut("+this.instanceName+")";
				htmloDayTable+=" onclick=CalendarCellsClick(this,"+this.instanceName+")>";
				htmloDayTable+="&nbsp;</td>"				
			}
		}
		htmloDayTable+="</tr>";	
	}
	htmloDayTable+="</tbody></table>";
	/// Today Button
	htmloButton="<div align='center' style='padding:3px'>"
	htmloButton+="<button id='"+this.sTODAYBTNID+"' style='width:30%;border:1px solid #BCD0DE;background-color:#eeeeee;cursor:hand'"
	htmloButton+=" onclick=CalendarTodayClick("+this.instanceName+")>"+this.oBtnTodayTitle+"</button>&nbsp;"
	
	htmloButton+="<button id='"+this.sCLEANBTNID+"' style='width:30%;border:1px solid #BCD0DE;background-color:#eeeeee;cursor:hand'"
	htmloButton+=" onclick=CalendarCleanClick("+this.instanceName+")>"+this.oBtnCleanTitle+"</button>&nbsp;"
	
	htmloButton+="<button style='width:30%;border:1px solid #BCD0DE;background-color:#eeeeee;cursor:hand'"
	htmloButton+=" onclick=CalendarCancel("+this.instanceName+")>"+this.oBtnCancelTitle+"</button> "
	htmloButton+="</div>"
	/// All
	htmlAll=htmlAll+htmloMonth+htmloYear+htmloDayTable+htmloButton+"</div>"
	+"<iframe id='DivShim' src='javascript:false;' scrolling='no' frameborder='0' style='position:absolute; top:0px; left:0px; display:none;'> </iframe>";
	
	document.write(htmlAll);
	this.Fill();	
}
function CalendarFill()			///
{
	var sMonth,sYear,sWeekDay,sToday,oTable,currRow,MaxDay,iDaySn,sIndex,rowIndex,cellIndex,oSelectMonth,oSelectYear
	sMonth=this.currDate.getMonth();
	sYear=this.currDate.getYear();
	sWeekDay=(new Date(sYear,sMonth,1)).getDay();
	sToday=this.currDate.getDate();
	iDaySn=1
	oTable=document.all[this.sTABLEID];
	currRow=oTable.rows[1];
	MaxDay=CalendarGetMaxDay(sYear,sMonth);
	
	oSelectMonth=document.all[this.sMONTHID]
	oSelectMonth.selectedIndex=sMonth;
	oSelectYear=document.all[this.sYEARID]
	for(i=0;i<oSelectYear.length;i++)
	{
		if(parseInt(oSelectYear.options[i].value)==sYear)oSelectYear.selectedIndex=i;
	}
	////
	for(rowIndex=1;rowIndex<=6;rowIndex++)
	{
		if(iDaySn>MaxDay)break;
		currRow = oTable.rows[rowIndex];
		cellIndex = 0;
		if(rowIndex==1)cellIndex = sWeekDay;
		for(;cellIndex<currRow.cells.length;cellIndex++)
		{
			if(iDaySn==sToday)
			{
				currRow.cells[cellIndex].innerHTML="<font color='"+this.selectedfontColor+"'><i><b>"+iDaySn+"</b></i></font>";
				this.oPreviousCell=currRow.cells[cellIndex];
			}
			else
			{
				currRow.cells[cellIndex].innerHTML=iDaySn;	
				currRow.cells[cellIndex].style.color=this.normalfontColor;
			}
			CalendarCellSetCss(0,currRow.cells[cellIndex]);
			iDaySn++;
			if(iDaySn>MaxDay)break;	
		}
	}
}
function CalendarRestore()					/// Clear Data
{	
	var i,j,oTable
	oTable=document.all[this.sTABLEID]
	for(i=1;i<oTable.rows.length;i++)
	{
		for(j=0;j<oTable.rows[i].cells.length;j++)
		{
			CalendarCellSetCss(0,oTable.rows[i].cells[j]);
			oTable.rows[i].cells[j].innerHTML="&nbsp;";
		}
	}	
}
function CalendarRefresh(newDate)					///
{
	this.currDate=newDate;
	this.Restore();	
	this.Fill();	
}
function CalendarCellsMsOver(oInstance)				/// Cell MouseOver
{
	var myCell = event.srcElement;
	CalendarCellSetCss(0,oInstance.oPreviousCell);
	if(myCell)
	{
		CalendarCellSetCss(1,myCell);
		oInstance.oPreviousCell=myCell;
	}
}
function CalendarCellsMsOut(oInstance)				////// Cell MouseOut
{
	var myCell = event.srcElement;
	CalendarCellSetCss(0,myCell);	
}
function CalendarYearChange(oInstance)				/// Year Change
{
	var sDay,sMonth,sYear,newDate
	sDay=oInstance.currDate.getDate();
	sMonth=oInstance.currDate.getMonth();
	sYear=document.all[oInstance.sYEARID].value
	newDate=new Date(sYear,sMonth,sDay);
	oInstance.Refresh(newDate);
}
function CalendarMonthChange(oInstance)				/// Month Change
{
	var sDay,sMonth,sYear,newDate
	sDay=oInstance.currDate.getDate();
	sMonth=document.all[oInstance.sMONTHID].value
	sYear=oInstance.currDate.getYear();
	newDate=new Date(sYear,sMonth,sDay);
	oInstance.Refresh(newDate);	
}
function CalendarCellsClick(oCell,oInstance)
{
	var sDay,sMonth,sYear,newDate
	sYear=oInstance.currDate.getFullYear();
	sMonth=oInstance.currDate.getMonth();
	sDay=oInstance.currDate.getDate();
	if(oCell.innerText!=" ")
	{
		sDay=parseInt(oCell.innerText);
		if(sDay!=oInstance.currDate.getDate())
		{
			newDate=new Date(sYear,sMonth,sDay);
			oInstance.Refresh(newDate);
		}
	}
	sDateString=sYear+oInstance.separator+CalendarDblNum(sMonth+1)+oInstance.separator+CalendarDblNum(sDay);		///return sDateString
	if(oInstance.oTaget.tagName.toLowerCase()=="input"){
		oInstance.oTaget.value = sDateString;
		oInstance.oTaget.focus();							//这个非常重要，用于日期选择之后的焦点事件触发
	}	
	CalendarCancel(oInstance);
	return sDateString;
}
function CalendarTodayClick(oInstance)				/// "Today" button Change
{	
	var sDay,sMonth,sYear,newDate
	oInstance.Refresh(new Date());
	sDay=new Date().getDate();
	sMonth=new Date().getMonth();
	sYear=new Date().getYear();
			
	sDateString=sYear+oInstance.separator+CalendarDblNum(sMonth+1)+oInstance.separator+CalendarDblNum(sDay);
	if(oInstance.oTaget.tagName.toLowerCase()=="input"){
		oInstance.oTaget.value = sDateString;
		oInstance.oTaget.focus();
	}
	CalendarCancel(oInstance);
	return sDateString;	
}
function CalendarCleanClick(oInstance)
{
	sDateString="";
	if(oInstance.oTaget.tagName.toLowerCase()=="input")oInstance.oTaget.value = sDateString;
	CalendarCancel(oInstance);
	return sDateString;	
}

function getDateString(oInputSrc,oInstance)
{
	if(oInputSrc&&oInstance) 
	{
		var CalendarDiv=document.all[oInstance.sDIVID];
		oInstance.oTaget=oInputSrc;
		CalendarDiv.style.pixelLeft=CalendargetPos(oInputSrc,"Left");
		CalendarDiv.style.pixelTop=CalendargetPos(oInputSrc,"Top") + oInputSrc.offsetHeight;
		CalendarDiv.style.display=(CalendarDiv.style.display=="none")?"":"none";
		var DivRef = document.all[oInstance.sDIVID];
	var IfrRef = document.getElementById('DivShim');
	DivRef.style.display = 'block';
	DivRef.style.zIndex=100;
	IfrRef.style.width = DivRef.offsetWidth;
	IfrRef.style.height = DivRef.offsetHeight;
	IfrRef.style.top = DivRef.style.top;
	IfrRef.style.left = DivRef.style.left;
	
	IfrRef.style.zIndex = DivRef.style.zIndex - 1;
	IfrRef.style.display = 'block';	
	}	
}
function CalendarCellSetCss(sMode,oCell)			/// Set Cell Css
{
	// sMode
	// 0: OnMouserOut 1: OnMouseOver 
	if(sMode)
	{
		oCell.style.border="1px solid #5589AA";
		oCell.style.backgroundColor="#BCD0DE";
	}
	else
	{
		oCell.style.border="1px solid #FFFFFF";
		oCell.style.backgroundColor="#FFFFFF";
	}	
}
function CalendarGetMaxDay(nowYear,nowMonth)			/// Get MaxDay of current month
{
	var nextMonth,nextYear,currDate,nextDate,theMaxDay
	nextMonth=nowMonth+1;
	if(nextMonth>11)
	{
		nextYear=nowYear+1;
		nextMonth=0;
	}
	else	
	{
		nextYear=nowYear;	
	}
	currDate=new Date(nowYear,nowMonth,1);
	nextDate=new Date(nextYear,nextMonth,1);
	theMaxDay=(nextDate-currDate)/(24*60*60*1000);
	return theMaxDay;
}
function CalendargetPos(el,ePro)				/// Get Absolute Position
{
	var ePos=0;
	while(el!=null)
	{		
		ePos+=el["offset"+ePro];
		el=el.offsetParent;
	}
	return ePos;
}
function CalendarDblNum(num)
{
	if(num < 10) 
		return "0"+num;
	else
		return num;
}
function CalendarCancel(oInstance)			///Cancel
{
	var CalendarDiv=document.all[oInstance.sDIVID];
	CalendarDiv.style.display="none";
	var IfrRef = document.getElementById('DivShim');
	IfrRef.style.display = 'none';		
}



		/**
		* @desc 格式化当前时期，字符串
		*  @author gzq
		**/
		var d, kdNnowDate="";
	   d = new Date();                        
	   kdNnowDate += d.getYear() + "-";
	   if(d.getMonth()+ 1<10){
	   		kdNnowDate += "0"+(d.getMonth()+ 1) + "-";
	   }else{
	   		kdNnowDate += (d.getMonth()+ 1) + "-";
	   }
	   if(d.getDate()<10){
	   		kdNnowDate += "0"+d.getDate();
	   }else{
	   		kdNnowDate += d.getDate();
	   }
	   /*kdNnowDate += " ";
	   if(d.getHours()<10){
	   		kdNnowDate += "0"+d.getHours() + ":";
	   }else{
	   		kdNnowDate += d.getHours() + ":";
	   }
	   if(d.getMinutes()<10){
	   		kdNnowDate += "0"+d.getMinutes();
	   }else{
	   		kdNnowDate += d.getMinutes();
	   }*/
	   

/* @desc 创建时间值
*  @author gzq
**/
function createKdTime(obj,space,defaultValue){
	for(var i=0;i<24;i++){
		if(i<10){
			obj.options[i]=new Option('0'+i,'0'+i)
		}else{
			obj.options[i]=new Option(i,i)
		}
		
		if(i==parseInt(defaultValue,10)){
			obj.options[i].selected=true;
		}
	}
}

/**
		* @desc 计算传入时间与当前时间相差小时数
		* @author gzq
		* @date 2008-12-06
		**/
		function accountTimeHours(dateStr,hours,dateStr_his,hours_his){
		
			try{
				var MinMilli = 1000 * 60;//1分钟
				var HrMilli = MinMilli * 60;//1小时
				var Days = HrMilli * 24;//1天

				var year_his = parseInt(dateStr_his.substr(0,4),10);
				var month_his = parseInt(dateStr_his.substr(5,2),10);
				var day_his = parseInt(dateStr_his.substr(8,2),10);
				var hour_his = parseInt(hours_his,10);
				var curdate = new Date(year_his,month_his,day_his,hour_his);
				var atime = curdate.getTime();
				
				var year = parseInt(dateStr.substr(0,4),10);
				var month = parseInt(dateStr.substr(5,2),10);
				var day = parseInt(dateStr.substr(8,2),10);
				var hour = parseInt(hours,10);
				
				var appointedDate = new Date(year,month,day,hour);
				var btime = appointedDate.getTime()

				var minmillis = Math.abs(btime-atime);
				var kdhours = Math.floor(minmillis / HrMilli);
				//var kdhours = parseFloat(minmillis / HrMilli).toFixed(1);
				return kdhours;
				
			}catch(e){
				alert("时间转换有误，请联系管理员!");
			}
		}