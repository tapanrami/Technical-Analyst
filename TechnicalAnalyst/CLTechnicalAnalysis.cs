using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalAnalyst
{
    public class CLTechnicalAnalysis
    {
        public decimal PivotPoint = 0.0m;
        public decimal PreviousDayHigh = 0.0m;
        public decimal PreviousDayLow = 0.0m;
        public decimal PrevicousDayClose = 0.0m;
              
        public async Task<List<decimal>> getSupportPoints()
        {
            List<decimal> supportPoints = new List<decimal>();
            try
            {
                await Task.Delay(0);
                supportPoints.Add((PivotPoint * 2) - PreviousDayHigh);
                supportPoints.Add((PivotPoint) - (PreviousDayHigh - PreviousDayLow));
                supportPoints.Add((PreviousDayLow) - 2 * (PreviousDayHigh - PivotPoint));
                //supportPoints[0] = ((PivotPoint * 2) - PreviousDayHigh);
                //supportPoints[1] = ((PivotPoint) - (PreviousDayHigh - PreviousDayLow));
                //supportPoints[2] = ((PreviousDayLow) - 2 * (PreviousDayHigh - PivotPoint));
                return supportPoints;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<decimal>> getResistancePoints()
        {
            List<decimal> resistancePoints = new List<decimal>();
            try
            {
				resistancePoints.Add((PivotPoint * 2) - PreviousDayHigh);
				resistancePoints.Add((PivotPoint) + (PreviousDayHigh - PreviousDayLow));
				resistancePoints.Add((PreviousDayLow) + 2 * (PreviousDayHigh - PivotPoint));

                //resistancePoints[0] = ((PivotPoint * 2) - PreviousDayLow);
                //resistancePoints[1] = ((PivotPoint) + (PreviousDayHigh - PreviousDayLow));
                //resistancePoints[2] = ((PreviousDayHigh) + 2 * (PivotPoint - PreviousDayLow));
                return resistancePoints;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CLTechnicalAnalysis(decimal previousDayHigh,decimal previousDayLow,decimal previcousDayClose)
        {
            PreviousDayHigh = previousDayHigh;
            PreviousDayLow = previousDayLow;
            PrevicousDayClose = previcousDayClose;

            PivotPoint = ((PreviousDayHigh + PreviousDayLow + PrevicousDayClose) / 3);
        }

    }
}
