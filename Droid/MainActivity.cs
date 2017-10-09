using Android.App;
using Android.Widget;
using Android.OS;
using TechnicalAnalyst;
using Android.Views.InputMethods;
using Android.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechnicalAnalyst.Droid
{
    [Activity(Label = "Technical Analyst", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            EditText etHigh = FindViewById<EditText>(Resource.Id.etHigh);
            EditText etLow = FindViewById<EditText>(Resource.Id.etLow);
            EditText etClose = FindViewById<EditText>(Resource.Id.etClose);

            Button btnCalc = FindViewById<Button>(Resource.Id.btnCalc);
			Button btnReset = FindViewById<Button>(Resource.Id.btnReset);

            TextView lblbp1 = FindViewById<TextView>(Resource.Id.lblbp1);
			TextView lblbp2 = FindViewById<TextView>(Resource.Id.lblbp2);
			TextView lblbp3 = FindViewById<TextView>(Resource.Id.lblbp3);

			TextView lblsl1 = FindViewById<TextView>(Resource.Id.lblsl1);
			TextView lblsl2 = FindViewById<TextView>(Resource.Id.lblsl2);
			TextView lblsl3 = FindViewById<TextView>(Resource.Id.lblsl3);

            btnCalc.Click+= async delegate {
                btnCalc.Enabled = false;
                btnReset.Enabled = false;
                if (this.CurrentFocus != null)
                {
                    InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
                    imm.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, 0);
                }
                if (!string.IsNullOrWhiteSpace(etHigh.Text) && !string.IsNullOrWhiteSpace(etLow.Text) && !string.IsNullOrWhiteSpace(etClose.Text))
                {
                    List<decimal> Points = new List<decimal>();
                    CLTechnicalAnalysis CLT = new CLTechnicalAnalysis(decimal.Parse(etHigh.Text), decimal.Parse(etLow.Text), decimal.Parse(etClose.Text));
                    Points = await CLT.getResistancePoints();
                    RunOnUiThread(() =>
                    {
                        lblbp1.Text = Points[0].ToString("C");
                        lblbp2.Text = Points[1].ToString("C");
                        lblbp3.Text = Points[2].ToString("C");
                    });

                    Points = await CLT.getSupportPoints();
                    RunOnUiThread(() =>
                    {
                        lblsl1.Text = Points[0].ToString("C");
                        lblsl2.Text = Points[1].ToString("C");
                        lblsl3.Text = Points[2].ToString("C");
                    });
                }
                else
                {
                    Toast t = Toast.MakeText(this, "Text Fields cannot be Blank", ToastLength.Short);
                    t.Show();
                }
                btnReset.Enabled = true;
                btnCalc.Enabled = true;
            };  

            btnReset.Click+= delegate {
                ViewGroup group = (ViewGroup)FindViewById(Resource.Id.tableLayout1);
                int count = group.ChildCount;
                for (int i = 0;i < count;i++)
                {
                    View view = group.GetChildAt(i);
                    if(view.GetType()==typeof(TableRow))
                    {
						ViewGroup groupRow = (ViewGroup)group.GetChildAt(i);
						int countRowChild = groupRow.ChildCount;
						for (int j = 0; j < countRowChild; j++)
						{
							View viewRow = groupRow.GetChildAt(j);
							if (viewRow.GetType() == typeof(EditText))
							{
								RunOnUiThread(() =>
								{
									((EditText)viewRow).Text = null;
								});
							}
						}
                    }
                }
                 this.Recreate();
            };
		}
    }
}

