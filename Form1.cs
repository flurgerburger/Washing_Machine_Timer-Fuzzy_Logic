namespace Washing_Machine_Timer_Fuzzy_Logic;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        cmbMethod.Items.Add("Sugeno Method");
        cmbMethod.Items.Add("Mamdani Method");
        cmbMethod.SelectedIndex = 0;
    }

    private void btnCalculate_Click(object sender, EventArgs e)
    {
        double load = (double)numLoad.Value;
        double soiling = (double)numSoiling.Value;
        double detergent = (double)numDetergent.Value;

        double time = 0.0;

        if (cmbMethod.SelectedItem.ToString() == "Sugeno Method")
        {
            time = CalculateSugeno(load, soiling, detergent);
        }
        else
        {
            time = CalculateMamdani(load, soiling, detergent);
        }

        lblResult.Text = $"Calculated Wash Time: {time:F2} Minutes";

    }

    private double CalculateSugeno(double load, double soiling, double detergent)
    {
        double loadLow = TriangularMembership(load, 0, 0, 5);
        double loadMed = TriangularMembership(load, 3, 5, 7);
        double loadHigh = TrapezoidalMembership(load, 5,7, 10, 10);

        double soilLight = TriangularMembership(soiling, 0, 0, 4);
        double soilMed = TriangularMembership(soiling, 2, 5, 8);
        double soilHeavy = TrapezoidalMembership(soiling, 5,7, 10, 10);

        double detLow = TriangularMembership(detergent, 0, 0, 30);
        double detNormal = TriangularMembership(detergent, 20, 50, 80);
        double detHigh = TrapezoidalMembership(detergent, 60,80, 100, 100);

        double r1 = Math.Min(loadLow, Math.Min(soilLight,detNormal));
        double cr1 = 15.0;

        double r2 = Math.Min(loadMed, Math.Min(soilMed,detNormal));
        double cr2 = 35.0;

        double r3 = Math.Max(loadHigh, Math.Max(soilHeavy,detLow));
        double cr3 = 50.0;

        double r4 = detHigh;
        double cr4 = 60.0;

        double num = (r1 * cr1) + (r2 * cr2) + (r3 * cr3) + (r4 * cr4);
        double den = r1 + r2 + r3 + r4;

        return den > 0 ? num / den : 0.0;
    }

    private double CalculateMamdani(double load, double soiling, double detergent)
    {
        double loadLow = TriangularMembership(load, 0, 0, 5);
        double loadMed = TriangularMembership(load, 3, 5, 7);
        double loadHigh = TrapezoidalMembership(load, 5 , 7 , 10, 10);

        double soilLight = TriangularMembership(soiling, 0, 0, 4);
        double soilMed = TriangularMembership(soiling, 2, 5, 8);
        double soilHeavy = TrapezoidalMembership(soiling, 5 , 7 , 10, 10);

        double detLow = TriangularMembership(detergent, 0, 0, 30);
        double detNormal = TriangularMembership(detergent, 20, 50, 80);
        double detHigh = TrapezoidalMembership(detergent, 60, 80, 100, 100);

        double r1 = Math.Min(loadLow, Math.Min(soilLight,detNormal));
        double r2 = Math.Min(loadMed, Math.Min(soilMed, detNormal));
        double r3 = Math.Max(loadHigh, Math.Max(soilHeavy,detNormal));
        double r4 = detHigh;

        double sumNum = 0.0;
        double sumDen = 0.0;
        double step = 1.0;

        for (double t = 0.0; t <= 60.0; t += step)
        {
            double outShort = TriangularMembership(t, 0, 0, 25.0);
            double outMed = TriangularMembership(t, 20.0, 35.0, 50.0);
            double outLong = TriangularMembership(t, 35.0, 50.0, 65.0);
            double outExtra = TriangularMembership(t, 50.0, 60.0, 60.0);

            double clip1 = Math.Min(r1, outShort);
            double clip2 = Math.Min(r2, outMed);
            double clip3 = Math.Min(r3, outLong);
            double clip4 = Math.Min(r4, outExtra);

            double aggregated = Math.Max(clip1, Math.Max(clip2,Math.Max(clip3,clip4)));

            sumNum += t * aggregated * step;
            sumDen += aggregated * step;
        }

        return sumDen > 0 ? sumNum / sumDen : 0.0;
    }

    private double TriangularMembership(double x, double a, double b, double c)
    {
        if (x <= a || x >= c) return 0.0;
        if (x == b) return 1.0;
        if (x > a && x < b) return (x - a) / (b - a);
        return (c - x) / (c - b);
    }

    private double TrapezoidalMembership(double x, double a, double b, double c ,double d)
    {
        if (x < a || x > d) return 0.0;
        if (x >= b && x <= c) return 1.0;
        if (x > a && x < b) return (x - a) / (b - a);
        return (d - x) / (d - c);
    }


    private void lblResult_Click(object sender, EventArgs e)
    {

    }

    private void label5_Click(object sender, EventArgs e)
    {

    }


}
