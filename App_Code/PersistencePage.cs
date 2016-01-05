using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

/*
	* 说    明：需要保存状态的页面需要继承此类，使用方法如下：
	* 
				1、保存状态时使用SavePageStateToSession方法，例子：
				private void Button2_Click(object sender, System.EventArgs e)
				{	
						this.SavePageStateToSession("mypagestate");
						Response.Redirect("WebForm2.aspx");	
				}
				mypagestate 为要将页面状态保存到的Session的名称
				
				2、恢复状态需向页面传递taoist_PageStateSession参数，例子
				private void Button1_Click(object sender, System.EventArgs e)
				{
					Response.Redirect("WebForm1.aspx?taoist_PageStateSession=mypagestate");
				}
				参数名为taoist_PageStateSession，其值为要恢复WebForm1.aspx页面所使用的Session
	* 
	* */

/// <summary>
/// 页面状态持久化。可保存和恢复页面的ViewState和表单内容
/// </summary>
namespace MKP.Common
{
    public class PersistencePage : System.Web.UI.Page
    {

        /// <summary>
        /// 是否从Session中恢复状态（此值来控制LoadPageStateFromPersistenceMedium的取值规则以及回发事件RaisePostBackEvent执行与否，防止状态恢复时重复执行控件事件）
        /// </summary>
        private bool taoist_IsLoadPageStateFromToSession = false;

        /// <summary>
        /// 页面状态保存到的Session
        /// </summary>
        private string taoist_PageStateSession = "";

        /// <summary>
        /// 保存页面状态（包括ViewState和表单内容）
        /// </summary>
        /// <param name="sessionName">存储页面状态的session</param>
        protected void SavePageStateToSession(string sessionName)
        {

            this.taoist_PageStateSession = sessionName;
            object state = base.LoadPageStateFromPersistenceMedium();

            //保存ViewState与表单内容
            Pair ps = new Pair();
            ps.First = state;
            ps.Second = base.DeterminePostBackMode();
            Session[sessionName] = ps;
        }

        /// <summary>
        /// 重写此方法，如果是从Session中恢复状态则无需触发控件事件
        /// </summary>
        /// <param name="sourceControl"></param>
        /// <param name="eventArgument"></param>
        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            if (!taoist_IsLoadPageStateFromToSession)
            {
                base.RaisePostBackEvent(sourceControl, eventArgument);
            }
        }

        /// <summary> 
        /// 重写此方法，只有taoist_IsLoadPageStateFromToSession为true时才从Session中还原ViewState。 
        /// </summary> 
        /// <returns></returns> 
        protected override object LoadPageStateFromPersistenceMedium()
        {
            object state;

            if (taoist_IsLoadPageStateFromToSession)
            {
                //还原ViewState
                Pair ps = (Pair)Session[taoist_PageStateSession];
                state = ps.First;
                //清空当前Session，保证只能恢复一次，防止无效恢复
                Session[taoist_PageStateSession] = null;
            }
            else
                state = base.LoadPageStateFromPersistenceMedium();

            return state;
        }

        /// <summary> 
        /// 重载此方法，增加表单恢复，并设置是否恢复Session
        /// </summary> 
        /// <returns></returns> 
        protected override System.Collections.Specialized.NameValueCollection DeterminePostBackMode()
        {

            System.Collections.Specialized.NameValueCollection formContent = base.DeterminePostBackMode();

            if (formContent == null)
            {
                //判断是否返回
                if (Request.QueryString["taoist_PageStateSession"] != null)
                {
                    taoist_PageStateSession = Convert.ToString(Request.QueryString["taoist_PageStateSession"]);
                    if (Session[taoist_PageStateSession] != null && !taoist_PageStateSession.Equals(""))
                    {
                        //设置需要还原ViewState
                        this.taoist_IsLoadPageStateFromToSession = true;

                        //先还原表单
                        Pair ps = Session[taoist_PageStateSession] as Pair;
                        if (ps != null)
                        {
                            formContent = ps.Second as System.Collections.Specialized.NameValueCollection;
                        }
                    }

                }
            }

            return formContent;
        }

    }
}