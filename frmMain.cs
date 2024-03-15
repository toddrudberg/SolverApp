using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using SolverPlatform;
using Electroimpact;
using System.IO;
using System.Threading;


namespace SolverApp
{
	public partial class frmMain : Form
	{
		#region Some Classes and Structures
    static public System.Collections.Specialized.StringDictionary cRanges = new System.Collections.Specialized.StringDictionary();

    private class cAxisLimits
    {
      private string name = "";
      private string limitstring = "";
      private double LimitMinus = 0;
      private double LimitPlus = 0;
      private double RangePlus = 0;
      private double RangeMinus = 0;
      private bool RangeSet = false;
      private bool LimitsEvaledOnce = false;

      /// <summary>
      /// Axis Name and its limit string.
      /// </summary>
      /// <param name="Name"></param>
      /// <param name="Limits">all = All data collected for this axis <\br> or 123-456 would allow only data between 123 and 456 to be evaluated</param>
      public cAxisLimits(string Name, string Limits)
      {
        name = Name;
        limitstring = Limits;
        ChangeAxisLimits(Limits);
      }
      public override string ToString()
      {
        return name + " " + limitstring;
      }

      public void ChangeAxisLimits(string limits)
      {
        limitstring = limits;
        csString cs = new csString(limitstring);
        cs.NukeWhiteSpace();
        if (cs.String.ToLower() == "all")
        {
          LimitMinus = RangeMinus;
          LimitPlus = RangePlus;
          return;
        }
        else
        {
          string st = cs.String.Replace("to", ",");
          string[] assholes = st.Split(',');
          if (assholes.Length == 2)
          {
            this.LimitMinus = double.TryParse(assholes[0], out LimitMinus) ? LimitMinus : 0.0;
            this.LimitPlus = double.TryParse(assholes[1], out LimitPlus) ? LimitPlus : 0.0;
            LimitsEvaledOnce = true;
          }
        }
      }

      private void EvaluateLimits(cPoints Data)
      {
        double min;
        double max;
        cPoint p = (cPoint)Data[0];
        min = (double)p.myAxisHash[name];
        max = (double)p.myAxisHash[name];
        for (int ii = 1; ii < Data.Count; ii++)
        {
          p = (cPoint)Data[ii];
          double v = (double)p.myAxisHash[name];
          if (v < min)
            min = v;
          if (v > max)
            max = v;
        }
        RangeMinus = min;
        RangePlus = max;
        if (!LimitsEvaledOnce)
        {
          LimitPlus = RangePlus;
          LimitMinus = RangeMinus;
        }

        RangeSet = true;
      }

      public string GetLimitString(cPoints Data)
      {
        if (Data == null)
          return "No Data Available!";
        if (!RangeSet)
        {
          EvaluateLimits(Data);
          if (!LimitsEvaledOnce)
          {
            LimitMinus = RangeMinus;
            LimitPlus = RangePlus;
          }
        }
        return LimitMinus.ToString("F3") + " to " + LimitPlus.ToString("F3");
      }

      public double MinusLimit
      {
        get { return this.LimitMinus; }
      }

      public double PlusLimit
      {
        get { return this.LimitPlus; }
      }

      public string AxisName
      {
        get { return name; }
      }

      public bool UseAll
      {
        get { return limitstring.ToLower() == "all"; }
      }
    }

    class cError
		{
			public double Xfact = 1.0;
			public double YFact = 1.0;
			public double ZFact = 1.0;
			public double X = 0;
			public double Y = 0;
			public double Z = 0;
			public double Total = 0;
			public int index = 0;
			public int count = 100;
			public bool Abort = false;
      public bool Pause = false;
      public double StandardDeviation = 0;
      public double AverageError = 0;
      public double framelessSumsq = 0;
      public double sqSumSqError = 0;

			public SolverPlatform.Optimize_Status OptimizeStatus = Optimize_Status.Optimal;
			public new string[] ToString()
			{
        string[] report = { "X Error: " + X.ToString("F10"), "Y Error: " + Y.ToString("F10"), "Z Error: " + Z.ToString("F10"), "Total:   " + Total.ToString("F10"), "StdDev: " + StandardDeviation.ToString("F10"), "Avg: " + AverageError.ToString("F10"), "A3s: " + (AverageError + 3.0 * StandardDeviation).ToString("F10")};//, "FlSumsq: " + framelessSumsq.ToString("F3") };
				return report;
			}
		}

    cError myError = new cError();
    cError myError1 = new cError();
    cError myError2 = new cError();

		class cPoint
		{
      public Hashtable myAxisHash = new Hashtable(); //This is a hack.  Some time later, the hash can be made autotmatic.

			public double Xtp
			{
				get { return (double)myAxisHash["Xmeas"]; }
			}
			public double Ytp
			{
        get { return (double)myAxisHash["Ymeas"]; }
      }
			public double Ztp
			{
        get { return (double)myAxisHash["Zmeas"]; }
      }
      public bool nXerr
      {
        get { return (bool)myAxisHash["nXerr"]; }
      }
      public bool nYerr
      {
        get { return (bool)myAxisHash["nYerr"]; }
      }
      public bool nZerr
      {
        get { return (bool)myAxisHash["nZerr"]; }
      }

			public double Xfwd;
			public double Yfwd;
			public double Zfwd;
			public double Afwd;
			public double Bfwd;
			public double Cfwd;

			public cPoint(XmlNodeReader elem, string[] AxisNames)
			{
				string sz;
        double v;
        for (int ii = 0; ii < AxisNames.Length; ii++)
        {
          sz = elem.GetAttribute(AxisNames[ii]);
          v = Double.TryParse(sz, out v) ? v : 0.0;
          myAxisHash.Add(AxisNames[ii], v);
        }

				sz = elem.GetAttribute("Xmeas");
        v = Double.TryParse(sz, out v) ? v : 0.0;
        myAxisHash.Add("Xmeas", v);

				sz = elem.GetAttribute("Ymeas");
        v = Double.TryParse(sz, out v) ? v : 0.0;
        myAxisHash.Add("Ymeas", v);

				sz = elem.GetAttribute("Zmeas");
        v = Double.TryParse(sz, out v) ? v : 0.0;
        myAxisHash.Add("Zmeas", v);

        sz = elem.GetAttribute("nXerr");
        myAxisHash.Add("nXerr", sz == "x");

        sz = elem.GetAttribute("nYerr");
        myAxisHash.Add("nYerr", sz == "x");

        sz = elem.GetAttribute("nZerr");
        myAxisHash.Add("nZerr", sz == "x");
      }

      public double GetMachinePosition(string AxisName)
      {
        if( myAxisHash.Contains(AxisName))
          return (double)myAxisHash[AxisName];
        else
          throw new Exception("error in: " + System.Reflection.MethodInfo.GetCurrentMethod().ToString() + " " + AxisName + " not in myAxisHash");
      }
		}

		class cPoints : System.Collections.ArrayList
		{
			public cPoints(string FileName, string[] AxisNames)
			{
				XmlNodeReader r;
				XmlDocument doc;
				try
				{
					doc = new XmlDocument();
					doc.Load(FileName);
					r = new XmlNodeReader(doc);
				}
				catch (Exception ex)
				{
					throw ex;
				}
				while (r.Read())
				{
					if (r.NodeType == XmlNodeType.Element)
					{
						if (r.Name == "Electroimpact_Flex-Track_part_program")
						{
						}
						if (r.Name == "Data")
						{
							cPoint ap = new cPoint(r, AxisNames);
							base.Add( ap );
						}
            if (r.Name == "Range")
            {
              r.MoveToFirstAttribute();
              if (!cRanges.ContainsKey(r.Name))
              {
                cRanges.Add(r.Name, r.Value);
              }
            }
					}
				}
				r.Close();
			}
      public cPoints()
      {
      }

      internal double GetMinValue(string p)
      {
        double ret = double.PositiveInfinity;
        for( int ii = 0; ii < this.Count; ii++)
        {
          cPoint ap = (cPoint)this[ii];
          double postion = (double)ap.myAxisHash[p];
          if (postion < ret)
            ret = postion;
        }
        return ret;
      }

      internal double GetMaxValue(string p)
      {
        double ret = double.NegativeInfinity;
        for (int ii = 0; ii < this.Count; ii++)
        {
          cPoint ap = (cPoint)this[ii];
          double postion = (double)ap.myAxisHash[p];
          if (postion > ret)
            ret = postion;
        }
        return ret;
      }
    }

    struct cAxisAttrib
    {
      public string axName;
      public string Attrib;
      public cAxisAttrib(string Name, string Attrib)
      {
        this.axName = Name;
        this.Attrib = Attrib;
      }
      public override string ToString()
      {
        return axName + ": " + Attrib;
      }
    }



		#endregion

		#region myMembers
    cSolverSequencer CurrentTry;
		cPoints myPoints;

    Electroimpact.Machine.cMachine myMachine;
    
    //Electroimpact.Machine.IMachine myMachine;
    //Electroimpact.Machine.IMachine myMachine1;
    //Electroimpact.Machine.IMachine myMachine2;
    //Electroimpact.Machine.cMachine myMachine;
    //Electroimpact.Machine.cRobot myMachine;
		System.Collections.ArrayList LinkKeys = new System.Collections.ArrayList();
		System.Collections.ArrayList AxisKeys = new System.Collections.ArrayList();
    System.Threading.Thread SolverThread;
    System.Collections.ArrayList myRanges = new System.Collections.ArrayList();
    ToolTip myTips = new ToolTip();

    System.Collections.ArrayList myAxisRanges = new System.Collections.ArrayList();

    private const double MultSmallAngle = 100.0;
    private const double MultSmallOffsets = 1.0;
    private const double MultScaleFactor = 100.0;

    private double runTimeHr = 0.0;
    private double runTimeMinute = 0.0;
    private double runTimeSec = 0.0;

    //private string szSolverFile = "";

    //private string generated_solver_filename = "";

    AutoResetEvent CaclulateErrorEvent1 = new AutoResetEvent(false);
    AutoResetEvent CaclulateErrorEvent2 = new AutoResetEvent(false);

    #region HelperClasses

    public class cFileHistory
    {
      public string NominalConfig;
      public string Config;
      public string Data;
      public string SolverRoutine;
      public string GeneratedSolverRoutine;
      public string AddlConfigFile;
      public cFileHistory()
      {
        NominalConfig = "none";
        Config = "none";
        Data = "none";
        SolverRoutine = "none";
        GeneratedSolverRoutine = "none";
        AddlConfigFile = "none";
      }
    }

    #endregion


    #endregion

    #region Constructor And frmMain_Load
    public frmMain()
		{
			InitializeComponent();
		}

    void lstAxisNameList_DoubleClick(object sender, EventArgs e)
    {
      
    }
		private void frmMain_Load(object sender, EventArgs e)
		{

      #if(!DEBUG)
         SolverPlatform.Environment _spe = new
         SolverPlatform.Environment("r0028082");
      #endif

         try
         {
           string file = MikesXmlSerializer.generateDefaultFilename("Electroimpact", "Solver", "files.xml");
           cFileHistory fh = new cFileHistory();
           if (File.Exists(file))
             fh = MikesXmlSerializer.Load<cFileHistory>(file);
           MikesXmlSerializer.Save(fh, file);
           this.lblFileConfigNom.Text = fh.NominalConfig;
           this.lblFileConfig.Text = fh.Config;
           this.lblFileData.Text = fh.Data;
           this.lblFileSolver.Text = fh.SolverRoutine;
           this.lblFileGenerated.Text = fh.GeneratedSolverRoutine;
        this.lblFileAdditional.Text = fh.AddlConfigFile;
           MikesXmlSerializer.Save(fh, file);

           this.PopulateVariableBoxes();
           this.lstAxisAttribs.Enabled = false;
           this.lstDHAttribs.Enabled = false;
           this.btnDH_checkall.Enabled = false;
           this.btnDH_uncheckall.Enabled = false;
           this.btnAxAt_checkall.Enabled = false;
           this.btnAxAt_uncheckall.Enabled = false;

         }
         catch(Exception ex)
         {
           throw ex;
         }
		}
		#endregion

		#region It is mostly here
		private void btnSolveIt_Click(object sender, EventArgs e)
		{

      int iterations = int.TryParse(tbMaxIterations.Text, out iterations) ? iterations : 100;
      this.myError.count = iterations;
      tbMaxIterations.Text = iterations.ToString("F0");


      this.CurrentTry = null;
			this.myMachine = null;
      if (!System.IO.File.Exists(this.lblFileConfig.Text))
        return;
      if (Robot(this.lblFileConfig.Text))
        this.myMachine = new Electroimpact.Machine.cRobot(this.lblFileConfig.Text, true);
      else
        this.myMachine = new Electroimpact.Machine.cMachine(this.lblFileConfig.Text, true);
      if (myPoints != null)
				myPoints.Clear();

      if (!System.IO.File.Exists(this.lblFileData.Text ))
        return;

			myPoints = new cPoints(this.lblFileData.Text, myMachine.GetAxisNames());

      lblSeqStepValue.Text = "1";

			SolverThread = new System.Threading.Thread(new System.Threading.ThreadStart(SolveMe));
			SolverThread.Name = "WorkerThread";
			pgStatus_Maximum(this.myError.count);
			SolverThread.Start();
		}

    private bool Robot(string ConfigFile)
    {
      bool robot = false;
      XmlNodeReader r;
      XmlDocument document;
      try
      {
        document = new XmlDocument();
        document.Load(ConfigFile);
        r = new XmlNodeReader(document);
      }
      catch (Exception ex)
      {
        throw (ex);
      }

      while (r.Read())
      {
        switch (r.NodeType)
        {
          case XmlNodeType.Element:
            {
              if (r.Name == "EITransform")
              {
                while (!robot && r.Read())
                {
                  if (r.NodeType == XmlNodeType.Element)
                  {
                    if (r.Name == "variables")
                    {
                      while (!robot && r.Read())
                      {
                        if (r.Name.ToLower() == "robot")
                          robot = true;
                      }
                    }
                  }
                }
              }
              break;
            }
        }
      }
      r.Close();
      if (robot)
        this.Text = "Electroimpact Solver for Robots";
      else
        this.Text = "Electroimpact Solver";
      return robot;
    }


		private void SolveMe()
		{

			#region Evaluating Range String



      bool RangeOK = SetRanges();

      if (!RangeOK)
        return;


      TrimMyPoints();

      #endregion

			#region Evaluating Items to solve

			#region Links
			LinkKeys.Clear();
			if (this.chkSolveDHLinks.Checked)
			{
				for (int ii = 0; ii < this.lstDHAttribs.Items.Count; ii++)
				{
          if (this.lstDHAttribs.GetItemChecked(ii))
          {
            LinkKeys.Add(this.lstDHAttribs.Items[ii].ToString());
          }
				}
			}
			#endregion

			#region Axes
			AxisKeys.Clear();
			if (this.chkSolveAxAttribs.Checked)
			{
				for (int ii = 0; ii < this.lstAxisAttribs.Items.Count; ii++)
				{
					if (this.lstAxisAttribs.GetItemChecked(ii))
						AxisKeys.Add((cAxisAttrib)lstAxisAttribs.Items[ii]);
				}
			}
			#endregion 

			#region Tables
			
      int CompStationsCount = 0;
      if (chkSolveCompTables.Checked)
      {
        for (int ii = 0; ii < lstCompTables.Items.Count; ii++)
        {

          if (lstCompTables.GetItemChecked(ii))
          {
            if (chkCompUseLimits.Checked && chkSolveCompTables.Checked)
            {
              cAxisLimits a = (cAxisLimits)lstCompTables.Items[ii];
              if (!a.UseAll)
                CompStationsCount += myMachine.GetCompensationTableStationCount(a.AxisName, a.MinusLimit, a.PlusLimit);
              else
              {
                double pluslim = myPoints.GetMaxValue(a.AxisName);
                double minuslim = myPoints.GetMinValue(a.AxisName);
                CompStationsCount += myMachine.GetCompensationTableStationCount(a.AxisName, minuslim, pluslim);
              }
            }
            else
            {
              string AxisName = ((cAxisLimits)lstCompTables.Items[ii]).AxisName;
              CompStationsCount += myMachine.GetCompensationTableStationCount(AxisName);
            }
          }
        }
      }

      int Number = 0;
      Number = this.chkCTX.Checked ? Number + 1 : Number;
      Number = this.chkCTY.Checked ? Number + 1 : Number;
      Number = this.chkCTZ.Checked ? Number + 1 : Number;
      Number = this.chkCTRoll.Checked ? Number + 1 : Number;
      Number = this.chkCTPitch.Checked ? Number + 1 : Number;
      Number = this.chkCTYaw.Checked ? Number + 1 : Number;


			#endregion

			#region Setting up problem and installing initial values
      try
      {
        if( LinkKeys.Count + AxisKeys.Count + CompStationsCount < 1 )
        {
          Exception ex = new Exception("You need to select something to solve!");
          throw (ex);
        }

        //MessageBox.Show("Solving this many variables: " + (LinkKeys.Count + AxisKeys.Count + CompStationsCount * Number).ToString());
        lblElementsToSolve.Text = "Solving this many variables: " + (LinkKeys.Count + AxisKeys.Count + CompStationsCount * Number).ToString() + " of 500 available.  There are " + myPoints.Count.ToString() + " included in this solution";
        using (Problem p = new Problem(Solver_Type.Minimize, LinkKeys.Count + AxisKeys.Count + CompStationsCount * Number, 1))
        {

          #region Variables and Axis Scaling 
          //bool useBounds = CurrentTry == null ? false : CurrentTry.UseLimits;
          for (int ii = 0; ii < LinkKeys.Count; ii++)
          {
            string VariableName = (string)LinkKeys[ii];
            p.VarDecision.InitialValue[ii] = myMachine.ReadAttribute(VariableName);

            //int indexof = VariableName.IndexOf("ToolPointOffset");
            //int indexof = VariableName.IndexOf("BaseShift");
            //if (indexof >= 0)
            if (VariableName.StartsWith("BaseShift"))
              p.VarDecision.InitialValue[ii] *= 10.0;
          }
          for (int ii = LinkKeys.Count; ii < LinkKeys.Count + AxisKeys.Count; ii++)
          { //We only ever do scaling anymore.
            double multiply = ((cAxisAttrib)AxisKeys[ii - LinkKeys.Count]).Attrib == "scalefactor" ? MultScaleFactor : 1.0;
            p.VarDecision.InitialValue[ii] = multiply * myMachine.ReadAxisAttribute(((cAxisAttrib)AxisKeys[ii - LinkKeys.Count]).axName, ((cAxisAttrib)AxisKeys[ii - LinkKeys.Count]).Attrib);
            //if (useBounds)
            //{
            //  p.VarDecision.LowerBound[ii] = multiply * -.1;
            //  p.VarDecision.UpperBound[ii] = multiply * .1;
            //}
          }
          #endregion

          #region CompensationTables
          if (chkSolveCompTables.Checked)
          {
            int Start = LinkKeys.Count + AxisKeys.Count;
            for (int ListNum = 0; ListNum < lstCompTables.Items.Count; ListNum++)
            {
              if (lstCompTables.GetItemChecked(ListNum))
              {

                int lower;
                int upper;
                int count;
                List<Electroimpact.Machine.cCompTable.cCompStation> table;
                if (chkCompUseLimits.Checked && chkSolveCompTables.Checked)
                {
                  cAxisLimits cax = (cAxisLimits)lstCompTables.Items[ListNum];
                  if (cax.UseAll)
                  {
                    double pluslim = myPoints.GetMaxValue(cax.AxisName);
                    double minuslim = myPoints.GetMinValue(cax.AxisName);
                    table = myMachine.GetCompensationTable(cax.AxisName, minuslim, pluslim, out lower, out upper);
                    //table = myMachine.GetCompensationTable(cax.AxisName);
                    //lower = 0;
                    //upper = table.Count - 1;
                  }
                  else
                    table = myMachine.GetCompensationTable(cax.AxisName, cax.MinusLimit, cax.PlusLimit, out lower, out upper);
                  count = upper - lower + 1;
                }
                else
                {
                  table = myMachine.GetCompensationTable(((cAxisLimits)lstCompTables.Items[ListNum]).AxisName);
                  lower = 0;
                  upper = table.Count - 1;
                  count = table.Count;
                }
                 

                //int End = Start + table.Count * Number;
                int End = Start + count * Number;
                int index = lower;
                for (int ii = Start; ii < End; ii += Number)
                {
                  Electroimpact.Machine.cCompTable.cCompStation cs = (Electroimpact.Machine.cCompTable.cCompStation)table[index];
                  int jj = 0;
                  if (chkCTX.Checked)
                  {
                    p.VarDecision.InitialValue[ii + jj] = cs.X * MultSmallOffsets;
                    //if (useBounds)
                    //{
                    //  p.VarDecision.UpperBound[ii + jj] = 1.0 * MultSmallOffsets;
                    //  p.VarDecision.LowerBound[ii + jj] = -1.0 * MultSmallOffsets;
                    //}
                    jj++;
                  }
                  if (chkCTY.Checked)
                  {
                    p.VarDecision.InitialValue[ii + jj] = cs.Y * MultSmallOffsets;
                    //if (useBounds)
                    //{
                    //  p.VarDecision.UpperBound[ii + jj] = 1.0 * MultSmallOffsets;
                    //  p.VarDecision.LowerBound[ii + jj] = -1.0 * MultSmallOffsets;
                    //}
                    jj++;
                  }
                  if (chkCTZ.Checked)
                  {
                    p.VarDecision.InitialValue[ii + jj] = cs.Z * MultSmallOffsets;
                    //if (useBounds)
                    //{
                    //  p.VarDecision.UpperBound[ii + jj] = 1.0 * MultSmallOffsets;
                    //  p.VarDecision.LowerBound[ii + jj] = -1.0 * MultSmallOffsets;
                    //}
                    jj++;
                  }
                  if (chkCTRoll.Checked)
                  {
                    p.VarDecision.InitialValue[ii + jj] = cs.rX * MultSmallAngle;
                    //if (useBounds)
                    //{
                    //  p.VarDecision.UpperBound[ii + jj] = .1 * MultSmallAngle;
                    //  p.VarDecision.LowerBound[ii + jj] = -.1 * MultSmallAngle;
                    //}
                    jj++;
                  }
                  if (chkCTPitch.Checked)
                  {
                    p.VarDecision.InitialValue[ii + jj] = cs.rY * MultSmallAngle;
                    //if (useBounds)
                    //{
                    //  p.VarDecision.UpperBound[ii + jj] = .1 * MultSmallAngle;
                    //  p.VarDecision.LowerBound[ii + jj] = -.1 * MultSmallAngle;
                    //}
                    jj++;
                  }
                  if (chkCTYaw.Checked)
                  {
                    p.VarDecision.InitialValue[ii + jj] = cs.rZ * MultSmallAngle;
                    //if (useBounds)
                    //{
                    //  p.VarDecision.UpperBound[ii + jj] = .1 * MultSmallAngle;
                    //  p.VarDecision.LowerBound[ii + jj] = -.1 * MultSmallAngle;
                    //}
                    jj++;
                  }
                  index++;
                }
                Start = End;
              }
            }
          }
          #endregion

          #region Problem Setup
          p.Evaluators[Eval_Type.Iteration].OnEvaluate += new EvaluateEventHandler(frmMain_OnEvaluate);
          p.Evaluators[Eval_Type.Function].OnEvaluate += new EvaluateEventHandler(Evaluator_Evaluate);
          



          if (CurrentTry == null)
          {
            
            if (this.radioLinear.Checked)
            {
              p.Engine = p.Engines[Engine.GRGName];
              p.ProblemType = Problem_Type.OptLP;
            }
            else if (this.radioNonLinear.Checked)
            {
              p.Engine = p.Engines[Engine.GRGName];
              p.ProblemType = Problem_Type.OptNLP;
            }
            else
            {
              p.Engine = p.Engines[Engine.GRGName];
              p.ProblemType = Problem_Type.OptLP;
            }

            //might want to add evolutionary here some day

          }
          else if (CurrentTry.EngineType == cSolverSequencer.eEngineType.Linear)
          {
            p.Engine = p.Engines[Engine.GRGName];
            p.ProblemType = Problem_Type.OptLP;
            this.radioLinear_CheckedChanged(true);
          }
          else if (CurrentTry.EngineType == cSolverSequencer.eEngineType.NonLinear)
          {
            p.Engine = p.Engines[Engine.GRGName];
            p.ProblemType = Problem_Type.OptNLP;
            this.radioNonLinear_CheckedChanged(true);
          }
          else if (CurrentTry.EngineType == cSolverSequencer.eEngineType.Evolutionary)
            p.Engine = p.Engines[Engine.EVOName];
          //Console.WriteLine();

          double timeout = double.TryParse(txtTimeOut.Text, out timeout) ? timeout : 361.0;
          txtTimeOut.Text = timeout.ToString("F0");
          p.Engine.Params["MaxTime"].Value = timeout;
          //Console.WriteLine(p.Engine.Params["MaxTime"].Value.ToString("F10"));
          p.Engine.Params["SearchOption"].Value = 1;
          //Console.WriteLine("SearchOption " + p.Engine.Params["SearchOption"].Value.ToString());
          p.Engine.Params["Derivatives"].Value = 1;
          //Console.WriteLine("Derivatives "+p.Engine.Params["Derivatives"].Value.ToString());
          p.Engine.Params["Estimates"].Value = 1;
          //Console.WriteLine("Estimates " + p.Engine.Params["Estimates"].Value.ToString());
          double stepsize = double.TryParse(txtStepSize.Text, out stepsize) ? stepsize : .001;
          p.Engine.Params["stepsize"].Value = stepsize; // double.Parse(cmbStepSize.Text);
          //Console.WriteLine("stepsize " + p.Engine.Params["stepsize"].Value.ToString("F10"));
          p.Engine.Params["Iterations"].Value = myError.count;
          //Console.WriteLine("Iterations " + p.Engine.Params["Iterations"].Value.ToString());
          p.Engine.Params["Precision"].Value = 0.000001;
          //Console.WriteLine("Precision " + p.Engine.Params["Precision"].Value.ToString("F10"));
          int autoscaling = chkAutoScale.Checked ? 1 : 0;
          p.Engine.Params["Scaling"].Value = autoscaling;
          Console.WriteLine(p.Engine.Params["Scaling"].Value.ToString());
          Console.WriteLine();
          Console.WriteLine("Solving this many points: " + myPoints.Count.ToString());
          //p.Engine.Params["convergance"].Value = .0001;// 10e-4 to 10e-9 affects how long it will chew on a problem.  Bigger the number, the less it will chew.
          //p.Engine.Params["multistart"].Value = 1;
          //p.Engine.Params["stepsize"].Value = .0001; //minimum guess size.
          //p.Engine.Params["Derivatives"].Value = 2;

          System.Threading.Monitor.Enter(myError);
          this.myError.index = 0;
          this.myError.Abort = false;
          System.Threading.Monitor.Exit(myError);

          p.Solver.Optimize();

          System.Threading.Monitor.Enter(myError);
          myError.OptimizeStatus = p.Solver.OptimizeStatus;
          System.Threading.Monitor.Exit(myError);
          #endregion
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        this.SolverThread.Abort();
      }

      #endregion
      //this.SolverThread.Abort();
      #endregion

    }

		Engine_Action frmMain_OnEvaluate(Evaluator evaluator)
		{
      Evaluator_Evaluate(evaluator); //Try the try.

      System.Threading.Monitor.Enter(this.myError);
			this.myError.index++;
      bool Exit = false; // this.myError.index >= this.myError.count;
			System.Threading.Monitor.Exit(this.myError);
			if (Exit || this.myError.Abort)
			{
				return Engine_Action.Stop;
			}
			return Engine_Action.Continue;
    }

    private Engine_Action Evaluator_Evaluate(Evaluator ev)
		{
			#region LinkKeys
			for (int ii = 0; ii < LinkKeys.Count; ii++)
			{
        double multiply = 1.0;
        if (((string)LinkKeys[ii]).StartsWith("BaseShift"))
          multiply *= 10.0;
        myMachine.WriteAttribute((string)LinkKeys[ii], ev.Problem.VarDecision.Value[ii] / multiply);
			}
			#endregion

			#region AxisKeys
			for (int ii = LinkKeys.Count; ii < LinkKeys.Count + AxisKeys.Count; ii++)
			{
        double multiply = ((cAxisAttrib)AxisKeys[ii - LinkKeys.Count]).Attrib == "scalefactor" ? 1 / MultScaleFactor : 1.0;
				myMachine.WriteAxisAttribute(((cAxisAttrib)AxisKeys[ii - LinkKeys.Count]).axName, ((cAxisAttrib)AxisKeys[ii - LinkKeys.Count]).Attrib, multiply * ev.Problem.VarDecision.Value[ii]);
			}
			#endregion

			#region CompTables
			int Number = 0;
			Number = this.chkCTX.Checked ? Number + 1 : Number;
			Number = this.chkCTY.Checked ? Number + 1 : Number;
			Number = this.chkCTZ.Checked ? Number + 1 : Number;
			Number = this.chkCTRoll.Checked ? Number + 1 : Number;
			Number = this.chkCTPitch.Checked ? Number + 1 : Number;
			Number = this.chkCTYaw.Checked ? Number + 1 : Number;

      int Start = LinkKeys.Count + AxisKeys.Count;
      if (chkSolveCompTables.Checked)
      {
        for (int ListNum = 0; ListNum < lstCompTables.Items.Count; ListNum++)
        {
          if (lstCompTables.GetItemChecked(ListNum))
          {
            int lower;
            int upper;
            int count;
            List<Electroimpact.Machine.cCompTable.cCompStation> table;
            if (chkCompUseLimits.Checked && chkSolveCompTables.Checked)
            {
              cAxisLimits cax = (cAxisLimits)lstCompTables.Items[ListNum];

              if (cax.UseAll)
              {
                double pluslim = myPoints.GetMaxValue(cax.AxisName);
                double minuslim = myPoints.GetMinValue(cax.AxisName);
                table = myMachine.GetCompensationTable(cax.AxisName, minuslim, pluslim, out lower, out upper);
                //table = myMachine.GetCompensationTable(cax.AxisName);
                //lower = 0;
                //upper = table.Count - 1;
              }
              else
                table = myMachine.GetCompensationTable(cax.AxisName, cax.MinusLimit, cax.PlusLimit, out lower, out upper);
              count = upper - lower + 1;
            }
            else
            {
              table = myMachine.GetCompensationTable(((cAxisLimits)lstCompTables.Items[ListNum]).AxisName);
              lower = 0;
              upper = table.Count - 1;
              count = table.Count;
            }
            int End = Start + count * Number;
            int index = lower;
            for (int ii = Start; ii < End; ii += Number)
            {
              Electroimpact.Machine.cCompTable.cCompStation cs = (Electroimpact.Machine.cCompTable.cCompStation)table[index];
              int jj = 0;
              if (chkCTX.Checked)
              {
                cs.X = ev.Problem.VarDecision.Value[ii + jj] / MultSmallOffsets;
                jj++;
              }
              if (chkCTY.Checked)
              {
                cs.Y = ev.Problem.VarDecision.Value[ii + jj] / MultSmallOffsets;
                jj++;
              }
              if (chkCTZ.Checked)
              {
                cs.Z = ev.Problem.VarDecision.Value[ii + jj] / MultSmallOffsets;
                jj++;
              }
              if (chkCTRoll.Checked)
              {
                cs.rX = ev.Problem.VarDecision.Value[ii + jj] / MultSmallAngle;
                jj++;
              }
              if (chkCTPitch.Checked)
              {
                cs.rY = ev.Problem.VarDecision.Value[ii + jj] / MultSmallAngle;
                jj++;
              }
              if (chkCTYaw.Checked)
              {
                cs.rZ = ev.Problem.VarDecision.Value[ii + jj] / MultSmallAngle;
                jj++;
              }
              table[index] = cs;
              index++;
            }
            myMachine.PutCompensationTable(((cAxisLimits)lstCompTables.Items[ListNum]).AxisName, table);
            Start = End;
          }
        }
      }
			#endregion

      //myMachine1 = myMachine;
      //myMachine2 = myMachine;

      //System.Threading.Thread tCalcError1 = new Thread(new System.Threading.ThreadStart(CalcErrorThread1));
      //tCalcError1.Name = "CalcErrorThread1";
      //tCalcError1.Priority = ThreadPriority.Highest;
      //tCalcError1.Start();

      //System.Threading.Thread tCalcError2 = new Thread(new System.Threading.ThreadStart(CalcErrorThread2));
      //tCalcError2.Name = "CalcErrorThread2";
      //tCalcError2.Priority = ThreadPriority.Highest;
      //tCalcError2.Start();
      
      //CaclulateErrorEvent1.WaitOne();
      //CaclulateErrorEvent2.WaitOne();

      //ev.Problem.FcnObjective.Value[0] = myError1.sqSumSqError * myError1.sqSumSqError + myError2.sqSumSqError * myError2.sqSumSqError;
      //ev.Problem.FcnObjective.Value[0] = myError.sqSumSqError * myError.sqSumSqError;
      
      double dogdare = CalculateError();
      ev.Problem.FcnObjective.Value[0] = dogdare;



      if (myError.Abort)
      {
        return Engine_Action.Stop;
      }
     

			return Engine_Action.Continue;
		}

    //private void CalcErrorThread1()
    //{
    //  CalculateError1();
    //  CaclulateErrorEvent1.Set();
    //}
    //private void CalcErrorThread2()
    //{
    //  CalculateError2();
    //  CaclulateErrorEvent2.Set();
    //}

    private void TrimMyPoints()
    {
      string[] myAxes = myMachine.GetAxisNames();
      cPoints ps = new cPoints();
      
      for (int jj = 0; jj < myRanges.Count; jj += 2)
      {
        for (int ii = (int)myRanges[jj]; ii <= (int)myRanges[jj + 1]; ii++)
        {
          cPoint p = (cPoint)myPoints[ii];
          bool OutOfRange = false;

          if (chkCompUseLimits.Checked && chkSolveCompTables.Checked)
          {
            for (int kk = 0; kk < lstCompTables.Items.Count; kk++)
            {
              if (lstCompTables.GetItemCheckState(kk) == System.Windows.Forms.CheckState.Checked)
              {
                cAxisLimits a = (cAxisLimits)lstCompTables.Items[kk];
                double min = a.MinusLimit;
                double max = a.PlusLimit;
                double v = p.GetMachinePosition(a.AxisName);
                if (v < min || v > max)
                {
                  //No reason to check anymore limits.
                  OutOfRange = true;
                  break;
                }
              }
            }
          }
          if (!OutOfRange)
          {
            ps.Add(p);
          }
        }
      }
      myPoints.Clear();
      myPoints = ps;
    }
    public class cTP
    {
      public double X = 0;
      public double Y = 0;
      public double Z = 0;
      public double A = 0;
      public double B = 0;
      public double C = 0;
    }

    private double zeroto360(double input)
    {
      double input_1 = zeroto180(input);
      if (input_1 < 0.0)
        input_1 += 360.0;
      return input_1;
    }

    private double zeroto180(double input)
    {
      double d2r = Math.PI / 180.0;
      double sini = Math.Sin(d2r * input);
      double cosi = Math.Cos(d2r * input);
      return Math.Atan2(sini, cosi) / d2r;
    }

    private double CalculateError()
    {
      double xErrSum = 0;
      double yErrSum = 0;
      double zErrSum = 0;
      double ErrSum = 0;
      //double framelessX = 0;
      //double framelessY = 0;
      //double framelessZ = 0;

      cTP lastMeasured = new cTP();
      cTP lastCalced = new cTP();
      cTP deltaDelta = new cTP();

      string[] myAxes = myMachine.GetAxisNames();

      for (int ii = 0; ii < myPoints.Count; ii++)
      {
        cPoint p = (cPoint)myPoints[ii];
        for (int kk = 0; kk < myAxes.Length; kk++)
          myMachine.WriteAxisPosition(myAxes[kk], p.GetMachinePosition(myAxes[kk]));
        double x = myMachine.X; //grabbing this value will induce a forward transform from values set by myMachine.WriteAxisPosition()
        double y = myMachine.Y;
        double z = myMachine.Z;

        //Display stuff
        p.Xfwd = x;
        p.Yfwd = y;
        p.Zfwd = z;
        p.Afwd = myMachine.rXrYrZ[0,0];
        p.Bfwd = myMachine.rXrYrZ[1,0];
        p.Cfwd = zeroto360(myMachine.rXrYrZ[2, 0]);

        double xErr = Math.Pow(p.Xtp - x, 2.0);
        double yErr = Math.Pow(p.Ytp - y, 2.0);
        double zErr = Math.Pow(p.Ztp - z, 2.0);
        if (!p.nXerr) xErrSum += xErr;
        if (!p.nYerr) yErrSum += yErr;
        if (!p.nZerr) zErrSum += zErr;
        ErrSum += Math.Sqrt(xErr * myError.Xfact + yErr * myError.YFact + zErr * myError.ZFact);

        //Frameless section
        //if (ii > 0)
        //{
        //  framelessX += Math.Pow((p.Xtp - lastMeasured.X) - (p.Xfwd - lastCalced.X), 2.0);
        //  framelessY += Math.Pow((p.Ytp - lastMeasured.Y) - (p.Yfwd - lastCalced.Y), 2.0);
        //  framelessZ += Math.Pow((p.Ztp - lastMeasured.Z) - (p.Zfwd - lastCalced.Z), 2.0);
        //}
        lastMeasured.X = p.Xtp;
        lastMeasured.Y = p.Ytp;
        lastMeasured.Z = p.Ztp;
        lastCalced.X = p.Xfwd;
        lastCalced.Y = p.Yfwd;
        lastCalced.Z = p.Zfwd;
      }

      double Error = 0.0;
      System.Threading.Monitor.Enter(this.myError);
      this.myError.X = Math.Sqrt(xErrSum) / (double)myPoints.Count;
      this.myError.Y = Math.Sqrt(yErrSum) / (double)myPoints.Count;
      this.myError.Z = Math.Sqrt(zErrSum) / (double)myPoints.Count;
      this.myError.Total = Math.Sqrt(xErrSum + yErrSum + zErrSum) / (double)myPoints.Count;
      this.myError.AverageError = ErrSum / (double)myPoints.Count;
      double StdDev = StandardDeviation(this.myError.AverageError);
      this.myError.StandardDeviation = StdDev;

      //double framelesserror = 0;
      //for (int ii = 0; ii < myPoints.Count - 1; ii++)
      //{
      //  for (int jj = ii + 1; jj < myPoints.Count; jj++)
      //  {
      //    if (jj != ii)
      //      framelesserror += diff((cPoint)myPoints[ii], (cPoint)myPoints[jj]);
      //  }
      //}

      if (opnSumSquares.Checked)
        Error = Math.Sqrt((myError.Xfact * xErrSum) + (myError.YFact * yErrSum) + (myError.ZFact * zErrSum));
      //else if (opnFlatten.Checked)
      //  Error = myError.AverageError + 5.0 * myError.StandardDeviation;
      //else
      //  Error = framelesserror;

      myError.sqSumSqError = Error;
      //myError.framelessSumsq = framelesserror;
      System.Threading.Monitor.Exit(this.myError);

      return Error * Error;
    }
    //private double CalculateError1()
    //{
    //  double xErrSum = 0;
    //  double yErrSum = 0;
    //  double zErrSum = 0;
    //  double ErrSum = 0;
    //  //double framelessX = 0;
    //  //double framelessY = 0;
    //  //double framelessZ = 0;

    //  cTP lastMeasured = new cTP();
    //  cTP lastCalced = new cTP();
    //  cTP deltaDelta = new cTP();

    //  string[] myAxes = myMachine1.GetAxisNames();

    //  for (int ii = 0; ii < myPoints.Count / 2; ii++)
    //  {
    //    cPoint p = (cPoint)myPoints[ii];
    //    for (int kk = 0; kk < myAxes.Length; kk++)
    //      myMachine1.WriteAxisPosition(myAxes[kk], p.GetMachinePosition(myAxes[kk]));
    //    double x = myMachine1.X;
    //    double y = myMachine1.Y;
    //    double z = myMachine1.Z;

    //    //Display stuff
    //    p.Xfwd = x;
    //    p.Yfwd = y;
    //    p.Zfwd = z;
    //    p.Afwd = myMachine1.rXrYrZ[0, 0];
    //    p.Bfwd = myMachine1.rXrYrZ[1, 0];
    //    p.Cfwd = zeroto360(myMachine1.rXrYrZ[2, 0]);


    //    double xErr = Math.Pow(p.Xtp - x, 2.0);
    //    double yErr = Math.Pow(p.Ytp - y, 2.0);
    //    double zErr = Math.Pow(p.Ztp - z, 2.0);
    //    xErrSum += xErr;
    //    yErrSum += yErr;
    //    zErrSum += zErr;
    //    ErrSum += Math.Sqrt(xErr * myError.Xfact + yErr * myError.YFact + zErr * myError.ZFact);

    //    //Frameless section
    //    //if (ii > 0)
    //    //{
    //    //  framelessX += Math.Pow((p.Xtp - lastMeasured.X) - (p.Xfwd - lastCalced.X), 2.0);
    //    //  framelessY += Math.Pow((p.Ytp - lastMeasured.Y) - (p.Yfwd - lastCalced.Y), 2.0);
    //    //  framelessZ += Math.Pow((p.Ztp - lastMeasured.Z) - (p.Zfwd - lastCalced.Z), 2.0);
    //    //}
    //    lastMeasured.X = p.Xtp;
    //    lastMeasured.Y = p.Ytp;
    //    lastMeasured.Z = p.Ztp;
    //    lastCalced.X = p.Xfwd;
    //    lastCalced.Y = p.Yfwd;
    //    lastCalced.Z = p.Zfwd;
    //  }

    //  double Error = 0.0;
    //  System.Threading.Monitor.Enter(this.myError1);
    //  this.myError1.X = Math.Sqrt(xErrSum) * 100.0 / (double)myPoints.Count;
    //  this.myError1.Y = Math.Sqrt(yErrSum) * 100.0 / (double)myPoints.Count;
    //  this.myError1.Z = Math.Sqrt(zErrSum) * 100.0 / (double)myPoints.Count;
    //  this.myError1.Total = Math.Sqrt(xErrSum + yErrSum + zErrSum) * 100.0 / (double)myPoints.Count;
    //  this.myError1.AverageError = ErrSum / ((double)myPoints.Count / 2.0);
    //  double StdDev = StandardDeviation(this.myError1.AverageError);
    //  this.myError1.StandardDeviation = StdDev;

    //  double framelesserror = 0;
    //  //for (int ii = 0; ii < myPoints.Count - 1; ii++)
    //  //{
    //  //  for (int jj = ii + 1; jj < myPoints.Count; jj++)
    //  //  {
    //  //    if (jj != ii)
    //  //      framelesserror += diff((cPoint)myPoints[ii], (cPoint)myPoints[jj]);
    //  //  }
    //  //}

    //  if (opnSumSquares.Checked)
    //    Error = Math.Sqrt((myError.Xfact * xErrSum) + (myError.YFact * yErrSum) + (myError.ZFact * zErrSum));
    //  //else if (opnFlatten.Checked)
    //  //  Error = myError1.AverageError + 5.0 * myError1.StandardDeviation;
    //  //else
    //  //  Error = framelesserror;

    //  myError1.sqSumSqError = Error;
    //  myError1.framelessSumsq = framelesserror;
    //  System.Threading.Monitor.Exit(this.myError1);

    //  return Error * Error;
    //}
    //private double CalculateError2()
    //{
    //  double xErrSum = 0;
    //  double yErrSum = 0;
    //  double zErrSum = 0;
    //  double ErrSum = 0;
    //  //double framelessX = 0;
    //  //double framelessY = 0;
    //  //double framelessZ = 0;

    //  cTP lastMeasured = new cTP();
    //  cTP lastCalced = new cTP();
    //  cTP deltaDelta = new cTP();

    //  string[] myAxes = myMachine2.GetAxisNames();

    //  for (int ii = myPoints.Count / 2; ii < myPoints.Count; ii++)
    //  {
    //    cPoint p = (cPoint)myPoints[ii];
    //    for (int kk = 0; kk < myAxes.Length; kk++)
    //      myMachine2.WriteAxisPosition(myAxes[kk], p.GetMachinePosition(myAxes[kk]));
    //    double x = myMachine2.X;
    //    double y = myMachine2.Y;
    //    double z = myMachine2.Z;

    //    //Display stuff
    //    p.Xfwd = x;
    //    p.Yfwd = y;
    //    p.Zfwd = z;
    //    p.Afwd = myMachine2.rXrYrZ[0, 0];
    //    p.Bfwd = myMachine2.rXrYrZ[1, 0];
    //    p.Cfwd = zeroto360(myMachine2.rXrYrZ[2, 0]);

    //    double xErr = Math.Pow(p.Xtp - x, 2.0);
    //    double yErr = Math.Pow(p.Ytp - y, 2.0);
    //    double zErr = Math.Pow(p.Ztp - z, 2.0);
    //    xErrSum += xErr;
    //    yErrSum += yErr;
    //    zErrSum += zErr;
    //    ErrSum += Math.Sqrt(xErr * myError.Xfact + yErr * myError.YFact + zErr * myError.ZFact);

    //    //Frameless section
    //    //if (ii > 0)
    //    //{
    //    //  framelessX += Math.Pow((p.Xtp - lastMeasured.X) - (p.Xfwd - lastCalced.X), 2.0);
    //    //  framelessY += Math.Pow((p.Ytp - lastMeasured.Y) - (p.Yfwd - lastCalced.Y), 2.0);
    //    //  framelessZ += Math.Pow((p.Ztp - lastMeasured.Z) - (p.Zfwd - lastCalced.Z), 2.0);
    //    //}
    //    lastMeasured.X = p.Xtp;
    //    lastMeasured.Y = p.Ytp;
    //    lastMeasured.Z = p.Ztp;
    //    lastCalced.X = p.Xfwd;
    //    lastCalced.Y = p.Yfwd;
    //    lastCalced.Z = p.Zfwd;
    //  }

    //  double Error = 0.0;
    //  System.Threading.Monitor.Enter(this.myError2);
    //  this.myError2.X = Math.Sqrt(xErrSum) * 100.0 / (double)myPoints.Count;
    //  this.myError2.Y = Math.Sqrt(yErrSum) * 100.0 / (double)myPoints.Count;
    //  this.myError2.Z = Math.Sqrt(zErrSum) * 100.0 / (double)myPoints.Count;
    //  this.myError2.Total = Math.Sqrt(xErrSum + yErrSum + zErrSum) * 100.0 / (double)myPoints.Count;
    //  this.myError2.AverageError = ErrSum / ((double)myPoints.Count / 2.0);
    //  double StdDev = StandardDeviation(this.myError2.AverageError);
    //  this.myError2.StandardDeviation = StdDev;

    //  double framelesserror = 0;
    //  //for (int ii = 0; ii < myPoints.Count - 1; ii++)
    //  //{
    //  //  for (int jj = ii + 1; jj < myPoints.Count; jj++)
    //  //  {
    //  //    if (jj != ii)
    //  //      framelesserror += diff((cPoint)myPoints[ii], (cPoint)myPoints[jj]);
    //  //  }
    //  //}

    //  if (opnSumSquares.Checked)
    //    Error = Math.Sqrt((myError.Xfact * xErrSum) + (myError.YFact * yErrSum) + (myError.ZFact * zErrSum));
    //  //else if (opnFlatten.Checked)
    //  //  Error = myError2.AverageError + 5.0 * myError2.StandardDeviation;
    //  //else
    //  //  Error = framelesserror;

    //  myError2.sqSumSqError = Error;
    //  myError2.framelessSumsq = framelesserror;
    //  System.Threading.Monitor.Exit(this.myError2);

    //  return Error * Error;
    //}

    private double diff(cPoint p1, cPoint p2)
    {
      double fwdDist = Math.Sqrt(Math.Pow(p1.Xfwd - p2.Xfwd, 2.0) + Math.Pow(p1.Yfwd - p2.Yfwd, 2.0) + Math.Pow(p1.Zfwd - p2.Zfwd, 2.0));
      double measDist = Math.Sqrt(Math.Pow(p1.Xtp - p2.Xtp, 2.0) + Math.Pow(p1.Ytp - p2.Ytp, 2.0) + Math.Pow(p1.Ztp - p2.Ztp, 2.0));
      return Math.Pow(fwdDist - measDist, 2.0);
    }

    private double StandardDeviation(double AverageErr)
    {
      double Deviation = 0;
      for (int ii = 0; ii < myPoints.Count; ii++)
      {
        cPoint pt = (cPoint)myPoints[ii];
        double radialError = Math.Sqrt(Math.Pow(pt.Xtp - pt.Xfwd, 2.0) * myError.Xfact +
                                       Math.Pow(pt.Ytp - pt.Yfwd, 2.0) * myError.YFact +
                                       Math.Pow(pt.Ztp - pt.Zfwd, 2.0) * myError.ZFact);
        Deviation += Math.Pow(radialError - AverageErr, 2.0);
      }
      Deviation /= (double)myPoints.Count;
      return Math.Sqrt(Deviation);
    }

    private double AverageError(double XerrorSum, double YerrorSum, double ZerrorSum, int Count)
    {
      return Math.Sqrt(XerrorSum + YerrorSum + ZerrorSum) / (double)Count;
    }

		#endregion

		#region User Interface Events
		private void btnSaveResults_Click(object sender, EventArgs e)
		{

      this.btnSolveOnce_Click(null, null);
      
      string FileOut = (this.lblFileConfig.Text);
      if (myMachine == null || !myMachine.IsHookedUp)
      {
        MessageBox.Show("Machine is null or not hooked up!");
        return;
      }
      if (FileOut.Contains("nominal"))
      {
        MessageBox.Show("Can't Save Over Nominal Config.  Check Config File Name");
        return;
      }



      this.myMachine.ToFile(FileOut, false,  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

      //this.myMachine.ToFile(FileOut + " error " + this.myError.Total.ToString("F10"));

      if (myPoints == null || myPoints.Count <= 0)
      {
        System.Windows.Forms.MessageBox.Show("No points to save!");
        return;
      }

      FileOut = this.lblFileData.Text;
			FileOut = FileOut.Substring(0, FileOut.Length - 4);
			FileOut += "out.xml";


			using (XmlTextWriter x = new XmlTextWriter(FileOut, System.Text.Encoding.UTF8))
			{
        string[] AxisNames = myMachine.GetAxisNames();
				x.Formatting = Formatting.Indented;
				x.WriteStartDocument();
				x.WriteStartElement("Electroimpact_SolverResultToolPoints");
				for (int ii = 0; ii < myPoints.Count; ii++)
				{
					x.WriteStartElement("Data");
					x.WriteAttributeString("Xtp", ((cPoint)myPoints[ii]).Xfwd.ToString("F4"));
					x.WriteAttributeString("Ytp", ((cPoint)myPoints[ii]).Yfwd.ToString("F4"));
					x.WriteAttributeString("Ztp", ((cPoint)myPoints[ii]).Zfwd.ToString("F4"));
					x.WriteAttributeString("Atp", ((cPoint)myPoints[ii]).Afwd.ToString("F4"));
					x.WriteAttributeString("Btp", ((cPoint)myPoints[ii]).Bfwd.ToString("F4"));
					x.WriteAttributeString("Ctp", ((cPoint)myPoints[ii]).Cfwd.ToString("F4"));
          for (int axisnum = 0; axisnum < AxisNames.Length; axisnum++)
          {
            x.WriteAttributeString(AxisNames[axisnum], ((cPoint)myPoints[ii]).GetMachinePosition(AxisNames[axisnum]).ToString("F4"));
          }
          //x.WriteAttributeString("Xcomp", (((cPoint)myPoints[ii]).Xfwd - ((cPoint)myPoints[ii]).GetMachinePosition("Xm")).ToString("F4"));
          //x.WriteAttributeString("Ycomp", (((cPoint)myPoints[ii]).Yfwd - ((cPoint)myPoints[ii]).GetMachinePosition("Ym")).ToString("F4"));
          //x.WriteAttributeString("Zcomp", (((cPoint)myPoints[ii]).Zfwd - ((cPoint)myPoints[ii]).GetMachinePosition("Zm")).ToString("F4"));
          x.WriteEndElement();
				}
				x.WriteEndElement();
				x.Flush();
				x.Close();
			}
		}

		private void tmrStatus_Tick(object sender, EventArgs e)
		{
			if (this.SolverThread != null && this.SolverThread.IsAlive)
			{
        this.pgStatus.Value = this.myError.index <= this.pgStatus.Maximum ? this.myError.index : this.pgStatus.Maximum;
        //lblSolverStatus_Text("solving...trial number: " + this.pgStatus.Value.ToString() + " of " + this.pgStatus.Maximum.ToString());
				this.grpSolverControls.Enabled = false;
				this.btnAbort.Enabled = true;

        string sIsPaused = "Solver is Running";

        if (myError.Pause)
        {
          this.btnUnPause.Enabled = true;
          sIsPaused = "Solver is Paused";
        }
        else
          this.btnUnPause.Enabled = false;


        lblSolverStatus_Text("solving...trial number: " + this.pgStatus.Value.ToString() + " of " + this.pgStatus.Maximum.ToString() + ", " + sIsPaused);

        if (!this.btnUnPause.Enabled)
          this.runTimeSec += 0.05;
        if(runTimeSec > 60.0)
        {
          runTimeSec = 0.0;
          runTimeMinute += 1.0;
        }
        if (runTimeMinute > 60.0)
        {
          runTimeMinute = 0.0;
          runTimeHr += 1.0;
        }
        this.lblRunTimeVal.Text = this.runTimeHr.ToString("F0") + " Hr " + this.runTimeMinute.ToString("F0") + " m " + this.runTimeSec.ToString("F0") + " s ";

			}
			else
			{
				string msg = "idle. ";
				if (this.myError.index > 0)
				{
					msg += "Last solution made " + this.myError.index.ToString() + " tries.";
					msg += " Solver Status: " + this.myError.OptimizeStatus.ToString();
				}
        if (this.lblSolverStatus.Text != msg)
          lblSolverStatus_Text(msg);
				this.grpSolverControls.Enabled = true;
				this.pgStatus.Value = this.pgStatus.Maximum;
				this.btnAbort.Enabled = false;
        this.runTimeHr = this.runTimeMinute = this.runTimeSec = 0.0;
        //this.lblRunTimeVal.Text = "0"; it is nice to see how long the sequence took to run...unless you happen to be watching when it quits, you would not know.
			}
			this.txtSolverStatus.Lines = this.myError.ToString();
		}

		private void mnuFileExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

    private void mnuFileConfigNom_Click(object sender, EventArgs e)
    {
      System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();
      if (System.IO.File.Exists(this.lblFileConfigNom.Text))
      {
        dlg.FileName = this.lblFileConfigNom.Text;
      }
      else
      {
        Electroimpact.FileIO.cFileOther f = new Electroimpact.FileIO.cFileOther();
        dlg.InitialDirectory = f.CurrentFolder() + "Data";
      }

      dlg.Multiselect = false;
      dlg.Filter = "Machine Nominal Config File|*nominal.config.xml";
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        if (dlg.FileName.Contains("nominal"))
        {
          string temp = "";
          string temp2;
          string dir;
          string[] temp_ar;


          //get file name including directory
          this.lblFileConfigNom.Text = dlg.FileName;

          //get just the filename without directory
          temp_ar = this.lblFileConfigNom.Text.Split('\\');
          temp2 = temp_ar[temp_ar.Length - 1];

          //get just the directory
          dir = this.lblFileConfigNom.Text.Replace(temp2,"");
          
          //create a working config file by removing spaces and the word "nominal"
          //if (temp2.Contains(".nominal"))
          //{
          //  temp2 = temp2.Replace(".nominal", "");
          //}
          //else
          //{
          //  temp2 = temp2.Replace("nominal", "");
          //}
          //temp2 = temp2.Replace(" ", "");

          ////add back in the directory
          //temp2 = dir + temp2;


          ////set label for working config file
          //this.lblFileConfig.Text = temp2;

          ////if working config file doesn't exist create it:
          //if (!System.IO.File.Exists(this.lblFileConfig.Text))
          //{
          //  System.IO.File.Copy(this.lblFileConfigNom.Text, this.lblFileConfig.Text);
          //}          

          
          this.PopulateVariableBoxes();
          SetFileHistory();
        }
      }
    }

    private void mnuReloadNomConfig_Click(object sender, EventArgs e)
    {
      //mnuReloadNomConfig

      //if working config file doesn't exist create it:
      if (System.IO.File.Exists(this.lblFileConfigNom.Text))
      {
        DialogResult drr2 = MessageBox.Show("Overwrite Working Config File with Nominal?",
           "Reload Nominal Config",
           MessageBoxButtons.YesNo);
        if (drr2 == DialogResult.Yes)
        {
          if (System.IO.File.Exists(this.lblFileConfig.Text))
          {
            System.IO.File.Delete(this.lblFileConfig.Text);
          }
          System.IO.File.Copy(this.lblFileConfigNom.Text, this.lblFileConfig.Text);
          this.PopulateVariableBoxes();
          SetFileHistory();
          btnSaveConfig_Click(sender, e);
          btnSolveOnce_Click(null, null);
        }
      }
    }

		private void mnuFileConfig_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();
      if (System.IO.File.Exists(this.lblFileConfig.Text))
      {
        dlg.FileName = this.lblFileConfig.Text;
      }
      else
      {
        Electroimpact.FileIO.cFileOther f = new Electroimpact.FileIO.cFileOther();
        dlg.InitialDirectory = f.CurrentFolder() + "Data";
      }


			dlg.Multiselect = false;
			dlg.Filter = "Machine Config File|*.config.xml";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
        if(dlg.FileName.Contains("nominal"))
        {
          MessageBox.Show("Can't Set Nominal As the Working Config");
          return;
        }

				this.lblFileConfig.Text = dlg.FileName;
				this.PopulateVariableBoxes();
				SetFileHistory();
        this.myMachine = null;
        if (!System.IO.File.Exists(this.lblFileConfig.Text))
          return;
        if (Robot(this.lblFileConfig.Text))
          this.myMachine = new Electroimpact.Machine.cRobot(this.lblFileConfig.Text, true);
        else
          this.myMachine = new Electroimpact.Machine.cMachine(this.lblFileConfig.Text, true);
			}
		}

    private void mnuFileAddConfigFile_Click(object sender, EventArgs e)
    {
      System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();
      if (System.IO.File.Exists(this.lblFileConfig.Text))
      {
        dlg.FileName = this.lblFileConfig.Text;
      }
      else
      {
        Electroimpact.FileIO.cFileOther f = new Electroimpact.FileIO.cFileOther();
        dlg.InitialDirectory = f.CurrentFolder() + "Data";
      }

      dlg.Multiselect = false;
      dlg.Filter = "Machine Config File|*.config.xml";
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        //this.lblFileConfig.Text = dlg.FileName;
        this.lblFileAdditional.Text = dlg.FileName;
        this.PopulateVariableBoxes();
        SetFileHistory();
        btnSaveResults_Click(null, null);
      }
    }

    private void mnuSolverFile_Click(object sender, EventArgs e)
    {
      System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();
      if (System.IO.File.Exists(this.lblFileSolver.Text))
      {
        dlg.FileName = this.lblFileSolver.Text;
      }
      else
      {
        Electroimpact.FileIO.cFileOther f = new Electroimpact.FileIO.cFileOther();
        dlg.InitialDirectory = f.CurrentFolder() + "Data";
      }


      dlg.Multiselect = false;
      dlg.Filter = "Solver File|*.solver.routine.xml";
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        Electroimpact.FileIO.cFileOther f = new Electroimpact.FileIO.cFileOther();
        if (f.FileExists(dlg.FileName))
        {
          this.lblFileSolver.Text = dlg.FileName;
          SetFileHistory();
        }
        else
          this.lblFileSolver.Text = "";
      }
    }
		private void PopulateVariableBoxes()
		{
			if (this.lblFileConfig.Text != null && this.lblFileConfig.Text != "")
			{
        bool checkforfiles = true;
        Electroimpact.FileIO.cFileOther cf = new Electroimpact.FileIO.cFileOther();
        if (!cf.FileExists(lblFileConfig.Text))
        {
          checkforfiles = false;
          this.lblFileConfig.Text = "None Selected";
        }
        if (!cf.FileExists(lblFileData.Text))
        {
          checkforfiles = false;
          lblFileData.Text = "None Selected";
        }
        if (!cf.FileExists(lblFileConfigNom.Text))
        {
          lblFileConfigNom.Text = "None Selected";
        }
        if (!cf.FileExists(lblFileAdditional.Text))
        {
          lblFileAdditional.Text = "None Selected";
        }
        if (!cf.FileExists(lblFileGenerated.Text))
        {
          lblFileGenerated.Text = "None Selected";
        }
        if (!cf.FileExists(lblFileSolver.Text))
        {
          lblFileSolver.Text = "None Selected";
        }

        if (!checkforfiles)
          return;

        if (Robot(this.lblFileConfig.Text))
          this.myMachine = new Electroimpact.Machine.cRobot(this.lblFileConfig.Text, true);
        else
          this.myMachine = new Electroimpact.Machine.cMachine(this.lblFileConfig.Text, true);

        if (cf.FileExists(lblFileAdditional.Text) && !lblFileAdditional.Text.ToLower().Contains("none selected"))
          this.myMachine.AddConfigFile(lblFileAdditional.Text);

        this.myPoints = new cPoints(lblFileData.Text, myMachine.GetAxisNames());

				this.lstDHAttribs.Items.Clear();
				string[] vars = myMachine.GetAttributeNames();
        lstDHAttribs.Items.Add("BaseShift_X");
        lstDHAttribs.Items.Add("BaseShift_Y");
        lstDHAttribs.Items.Add("BaseShift_Z");
        lstDHAttribs.Items.Add("BaseShift_rX");
        lstDHAttribs.Items.Add("BaseShift_rY");
        lstDHAttribs.Items.Add("BaseShift_rZ");
        for (int ii = 0; ii < vars.Length; ii++)
				{
          if (vars[ii].IndexOf("BaseShift") < 0)
            this.lstDHAttribs.Items.Add(vars[ii], false);
				}

        this.lstAxisAttribs.Items.Clear();
        string[] axnames = myMachine.GetAxisNames(false);
				for (int ii = 0; ii < axnames.Length; ii++)
				{
          if (myMachine.ContainsAxisAttribute(axnames[ii], "scalefactor"))
						this.lstAxisAttribs.Items.Add(new cAxisAttrib(axnames[ii], "scalefactor"));
				}
        string[] ComptableAxisnames;
        lstCompTables.Items.Clear();
        ComptableAxisnames = myMachine.GetCompensationTableAxisNames;
        for (int ii = 0; ii < ComptableAxisnames.Length; ii++)
        {
          cAxisLimits cal = new cAxisLimits(ComptableAxisnames[ii], "all");
          lstCompTables.Items.Add(cal);
        }
			}
		}

    System.Collections.ArrayList myAxisAttribs = new System.Collections.ArrayList();

		private void mnuFileData_Click(object sender, EventArgs e)
		{
      System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();
      if (System.IO.File.Exists(this.lblFileData.Text))
      {
        dlg.FileName = this.lblFileConfig.Text;
      }
      else
      {
        Electroimpact.FileIO.cFileOther f = new Electroimpact.FileIO.cFileOther();
        dlg.InitialDirectory = f.CurrentFolder() + "Data";
      }
			dlg.Multiselect = false;
			dlg.Filter = "Machine Tracker Data|*.data.xml";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
        cRanges.Clear(); //clear the named ranges so that they can be reloaded

				this.lblFileData.Text = dlg.FileName;
        PopulateVariableBoxes();
				SetFileHistory();
			}

		}

		private void btnAbort_Click(object sender, EventArgs e)
		{
      System.Threading.Monitor.Enter(myError);
      this.myError.Abort = true;
      System.Threading.Monitor.Exit(myError);


      //SolverThread.Abort();
		}

		private void txtXFactor_Leave(object sender, EventArgs e)
		{
			double dog = double.TryParse(txtXFactor.Text, out dog) ? dog : 1.0;
			txtXFactor.Text = dog.ToString("F3");
      System.Threading.Monitor.Enter(myError);
			this.myError.Xfact = dog;
      System.Threading.Monitor.Exit(myError);
    }
		void txtYFactor_Leave(object sender, System.EventArgs e)
		{
			double dog = double.TryParse(txtYFactor.Text, out dog) ? dog : 1.0;
			txtYFactor.Text = dog.ToString("F3");
      System.Threading.Monitor.Enter(myError);
			this.myError.YFact = dog;
      System.Threading.Monitor.Exit(myError);
    }
		void txtZFactor_Leave(object sender, System.EventArgs e)
		{
			double dog = double.TryParse(txtZFactor.Text, out dog) ? dog : 1.0;
			txtZFactor.Text = dog.ToString("F3");
      System.Threading.Monitor.Enter(myError);
			this.myError.ZFact = dog;
      System.Threading.Monitor.Exit(myError);
    }
    private void chkSolveDHLinks_CheckedChanged(object sender, EventArgs e)
		{
			this.lstDHAttribs.Enabled = chkSolveDHLinks.Checked;

      this.btnDH_checkall.Enabled = chkSolveDHLinks.Checked;
      this.btnDH_uncheckall.Enabled = chkSolveDHLinks.Checked;

		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			this.lstAxisAttribs.Enabled = chkSolveAxAttribs.Checked;
      this.btnAxAt_checkall.Enabled = chkSolveAxAttribs.Checked;
      this.btnAxAt_uncheckall.Enabled = chkSolveAxAttribs.Checked;
		}

		private void btnSolveOnce_Click(object sender, EventArgs e)
		{
      //Compute error from current solution


      bool UseAxisLimits = chkCompUseLimits.Checked;

      if (myMachine == null) //only do this if you don't have a machine.  This way you get the latest changes to the machine when you SolveOnce.
      {
        if (System.IO.File.Exists(lblFileConfig.Text))
        {
          if (Robot(this.lblFileConfig.Text))
            this.myMachine = new Electroimpact.Machine.cRobot(this.lblFileConfig.Text, true);
          else
            this.myMachine = new Electroimpact.Machine.cMachine(this.lblFileConfig.Text, true);
        }
        else
        {
          if (sender != null)
            MessageBox.Show("Machine file does not exist!");
          return;
        }
      }


      if (!System.IO.File.Exists(lblFileData.Text))
      {
        if (sender != null)
          MessageBox.Show("Data File does not exist!");
        return;
      }
      myPoints = null;
			myPoints = new cPoints(this.lblFileData.Text, myMachine.GetAxisNames());
			this.myRanges.Clear();
			int Start = 0;
			int End = this.myPoints.Count - 1;
			this.myRanges.Add(Start);
			this.myRanges.Add(End);

      chkCompUseLimits_Checked(false);
      this.CalculateError();
      chkCompUseLimits_Checked(UseAxisLimits);
		}

		private void chkXscaler_CheckedChanged(object sender, EventArgs e)
		{
			if (chkXscaler.Checked)
			{
				this.txtXFactor.Enabled = true;
        System.Threading.Monitor.Enter(myError);
				this.myError.Xfact = double.TryParse(this.txtXFactor.Text, out this.myError.Xfact) ? this.myError.Xfact : 1.0;
        System.Threading.Monitor.Exit(myError);
      }
			else
			{
				this.txtXFactor.Enabled = false;
        System.Threading.Monitor.Enter(myError);
        this.myError.Xfact = 0;
        System.Threading.Monitor.Exit(myError);
      }
		}

		private void chkYscaler_CheckedChanged(object sender, EventArgs e)
		{
			if (chkYscaler.Checked)
			{
				this.txtYFactor.Enabled = true;
        System.Threading.Monitor.Enter(myError);
        this.myError.YFact = double.TryParse(this.txtYFactor.Text, out this.myError.YFact) ? this.myError.YFact : 1.0;
        System.Threading.Monitor.Exit(myError);
      }
			else
			{
				this.txtYFactor.Enabled = false;
        System.Threading.Monitor.Enter(myError);
        this.myError.YFact = 0;
        System.Threading.Monitor.Exit(myError);
      }
		}

		private void chkZscaler_CheckedChanged(object sender, EventArgs e)
		{
			if (chkZscaler.Checked)
			{
				this.txtZFactor.Enabled = true;
        System.Threading.Monitor.Enter(myError);
        this.myError.ZFact = double.TryParse(this.txtZFactor.Text, out this.myError.ZFact) ? this.myError.ZFact : 1.0;
        System.Threading.Monitor.Exit(myError);
      }
			else
			{
				this.txtZFactor.Enabled = false;
        System.Threading.Monitor.Enter(myError);
        this.myError.ZFact = 0;
        System.Threading.Monitor.Exit(myError);
      }
		}

		private void btnCTcheckall_Click(object sender, EventArgs e)
		{
			this.chkCTX.Checked = true;
			this.chkCTY.Checked = true;
			this.chkCTZ.Checked = true;
			this.chkCTRoll.Checked = true;
			this.chkCTPitch.Checked = true;
			this.chkCTYaw.Checked = true;
		}

		private void btnCTuncheckall_Click(object sender, EventArgs e)
		{
			this.chkCTX.Checked = false;
			this.chkCTY.Checked = false;
			this.chkCTZ.Checked = false;
			this.chkCTRoll.Checked = false;
			this.chkCTPitch.Checked = false;
			this.chkCTYaw.Checked = false;
		}

    private delegate void dchkXerr_Checked(bool value);
    private void chkXerr_Checked(bool value)
    {
      if (chkXscaler.InvokeRequired)
        this.chkXscaler.BeginInvoke(new dchkXerr_Checked(chkXerr_Checked), new object[] { value });
      else
        this.chkXscaler.Checked = value;
    }

    private delegate void dchkYerr_Checked(bool value);
    private void chkYerr_Checked(bool value)
    {
      if (chkYscaler.InvokeRequired)
        this.chkYscaler.BeginInvoke(new dchkYerr_Checked(chkYerr_Checked), new object[] { value });
      else
        this.chkYscaler.Checked = value;
    }

    private delegate void dchkZerr_Checked(bool value);
    private void chkZerr_Checked(bool value)
    {
      if (chkZscaler.InvokeRequired)
        this.chkZscaler.BeginInvoke(new dchkZerr_Checked(chkZerr_Checked), new object[] { value });
      else
        this.chkZscaler.Checked = value;
    }

    private delegate void dchkCTX_Checked(bool value);
    private void chkCTX_Checked(bool value)
    {
      if (chkCTX.InvokeRequired)
        this.chkCTX.BeginInvoke(new dchkCTX_Checked(chkCTX_Checked), new object[] { value });
      else
        this.chkCTX.Checked = value;
    }

    private delegate void dchkCTY_Checked(bool value);
    private void chkCTY_Checked(bool value)
    {
      if (chkCTY.InvokeRequired)
        this.chkCTY.BeginInvoke(new dchkCTY_Checked(chkCTY_Checked), new object[] { value });
      else
        this.chkCTY.Checked = value;
    }

    private delegate void dchkSolveDHLinks_Checked(bool value);
    private void chkSolveDHLinks_Checked(bool value)
    {
      if (chkSolveDHLinks.InvokeRequired)
        chkSolveDHLinks.BeginInvoke(new dchkSolveDHLinks_Checked(chkSolveDHLinks_Checked), new object[] { value });
      else
        chkSolveDHLinks.Checked = value;
    }

    private delegate void dchkSolveAxAttribs_Checked(bool value);
    private void chkSolveAxAttribs_Checked(bool value)
    {
      if (chkSolveAxAttribs.InvokeRequired)
        chkSolveAxAttribs.BeginInvoke(new dchkSolveAxAttribs_Checked(chkSolveAxAttribs_Checked), new object[] { value });
      else
        chkSolveAxAttribs.Checked = value;
    }

    private delegate void dchkSolveCompTables_Checked(bool value);
    private void chkSolveCompTables_Checked(bool value)
    {
      if (chkSolveCompTables.InvokeRequired)
        chkSolveCompTables.BeginInvoke(new dchkSolveCompTables_Checked(chkSolveCompTables_Checked), new object[] { value });
      else
        chkSolveCompTables.Checked = value;
    }


    private delegate void dchkCTZ_Checked(bool value);
    private void chkCTZ_Checked(bool value)
    {
      if (chkCTZ.InvokeRequired)
        this.chkCTZ.BeginInvoke(new dchkCTZ_Checked(chkCTZ_Checked), new object[] { value });
      else
        this.chkCTZ.Checked = value;
    }


    private delegate void dchkCTRoll_Checked(bool value);
    private void chkCTRoll_Checked(bool value)
    {
      if (chkCTRoll.InvokeRequired)
        this.chkCTRoll.BeginInvoke(new dchkCTRoll_Checked(chkCTRoll_Checked), new object[] { value });
      else
        this.chkCTRoll.Checked = value;
    }

    private delegate void dchkCTPitch_Checked(bool value);
    private void chkCTPitch_Checked(bool value)
    {
      if (chkCTPitch.InvokeRequired)
        this.chkCTPitch.BeginInvoke(new dchkCTPitch_Checked(chkCTPitch_Checked), new object[] { value });
      else
        this.chkCTPitch.Checked = value;
    }

    private delegate void dchkCompUseLimits_Checked(bool value);
    private void chkCompUseLimits_Checked(bool value)
    {
      if (chkCompUseLimits.InvokeRequired)
        this.chkCompUseLimits.BeginInvoke(new dchkCompUseLimits_Checked(chkCompUseLimits_Checked), new object[] { value });
      else
        this.chkCompUseLimits.Checked = value;
    }

    private delegate void dchkCTYaw_Checked(bool value);
    private void chkCTYaw_Checked(bool value)
    {
      if (chkCTYaw.InvokeRequired)
        this.chkCTYaw.BeginInvoke(new dchkCTYaw_Checked(chkCTYaw_Checked), new object[] { value });
      else
        this.chkCTYaw.Checked = value;
    }

    private delegate void dopnSumSquares_CheckedChanged(bool value);
    private void opnSumSquares_CheckedChanged(bool value)
    {
      if (opnSumSquares.InvokeRequired)
        this.opnSumSquares.BeginInvoke(new dopnSumSquares_CheckedChanged(opnSumSquares_CheckedChanged), new object[] { value });
      else
        this.opnSumSquares.Checked = value;
    }


    private delegate void dopnExperimental_CheckedChanged(bool value);
    private void opnExperimental_CheckedChanged(bool value)
    {
      if (opnExperimental.InvokeRequired)
        this.opnExperimental.BeginInvoke(new dopnExperimental_CheckedChanged(opnExperimental_CheckedChanged), new object[] { value });
      else
        this.opnExperimental.Checked = value;
    }



    private delegate void dradioLinear_CheckedChanged(bool value);
    private void radioLinear_CheckedChanged(bool value)
    {
      if (radioLinear.InvokeRequired)
        this.radioLinear.BeginInvoke(new dradioLinear_CheckedChanged(radioLinear_CheckedChanged), new object[] { value });
      else
        this.radioLinear.Checked = value;
    }

    private delegate void dradioNonLinear_CheckedChanged(bool value);
    private void radioNonLinear_CheckedChanged(bool value)
    {
      if (radioNonLinear.InvokeRequired)
        this.radioNonLinear.BeginInvoke(new dradioNonLinear_CheckedChanged(radioNonLinear_CheckedChanged), new object[] { value });
      else
        this.radioNonLinear.Checked = value;
    }

    private delegate void dchk_lblSeqStepValue(string value);
    private void chk_lblSeqStepValue(string value)
    {
      if (lblSeqStepValue.InvokeRequired)
        this.lblSeqStepValue.BeginInvoke(new dchk_lblSeqStepValue(chk_lblSeqStepValue), new object[] { value });
      else
        this.lblSeqStepValue.Text = value;
    }

    private delegate void dtxtRange_Text(string Text);
    private void txtRange_Text(string Text)
    {
      if (txtRange.InvokeRequired)
        this.txtRange.BeginInvoke(new dtxtRange_Text(txtRange_Text), new object[] { Text });
      else
        this.txtRange.Text = Text;
    }

    private delegate void dpgStatus_Maximum(int Value);
    private void pgStatus_Maximum(int Value)
    {
      if (pgStatus.InvokeRequired)
        this.pgStatus.BeginInvoke(new dpgStatus_Maximum(pgStatus_Maximum), new object[] { Value });
      else
        pgStatus.Maximum = Value;
    }

    private delegate void dlblSolverStatus_Text(string Text);
    private void lblSolverStatus_Text(string Text)
    {
      if (lblSolverStatus.InvokeRequired)
        lblSolverStatus.BeginInvoke(new dlblSolverStatus_Text(lblSolverStatus_Text), new object[] { Text });
      else
        lblSolverStatus.Text = Text;
    }

    private delegate void dlstCompTables_Update(string Text);
    private void lstCompTables_Update(string Text)
    {
      if (lstCompTables.InvokeRequired)
        lstCompTables.BeginInvoke(new dlstCompTables_Update(lstCompTables_Update), new object[] { Text });
      else
      {
        if (Text == null || Text.ToLower() == "all")
        {
          for (int ii = 0; ii < lstCompTables.Items.Count; ii++)
          {
            lstCompTables.SetItemChecked(ii, true);
            lstCompTables.SelectedIndex = ii;
          }
        }
        else
        {
          string[] AxisNames = Text.Split(',');
          for (int ii = 0; ii < lstCompTables.Items.Count; ii++)
          {
            lstCompTables.SetItemChecked(ii, false);
            cAxisLimits cax = (cAxisLimits)lstCompTables.Items[ii];
            for (int jj = 0; jj < AxisNames.Length; jj++)
            {
              string[] LimitString = AxisNames[jj].Split(' ');
              if (LimitString.Length == 1)
              {//Business as usual.
                if (cax.AxisName == AxisNames[jj])
                {
                  lstCompTables.SetItemChecked(ii, true);
                  lstCompTables.SelectedIndex = ii;
                  cax.ChangeAxisLimits("all");
                  //lstCompTables_SelectedIndexChanged(null, null); //updates axis limits...required!
                  break;
                }
              }
              else if (LimitString.Length == 4)
              {
                if (cax.AxisName == LimitString[0])
                {
                  lstCompTables.SetItemChecked(ii, true);
                  cax.ChangeAxisLimits(LimitString[1] + " to " + LimitString[3]);
                  lstCompTables.SelectedIndex = ii;
                }
              }
              else
                throw new Exception("Unexpected CompTable TableString...Error in " + System.Reflection.MethodInfo.GetCurrentMethod().ToString());
            }
          }
        }
      }
    }

    private delegate void dlstDHAttribs_Update(string Text);
    private void lstDHAttribs_Update(string Text)
    {
      if (lstDHAttribs.InvokeRequired)
        lstDHAttribs.BeginInvoke(new dlstDHAttribs_Update(lstDHAttribs_Update), new object[] { Text });
      else
      {
        //Clear them all to start
        for (int ii = 0; ii < lstDHAttribs.Items.Count; ii++)
          lstDHAttribs.SetItemChecked(ii, false);
        //if there is something wrong do this...
        if (Text == null || Text.ToLower() == "all")
        {
          for (int ii = 0; ii < lstDHAttribs.Items.Count; ii++)
          {
            lstDHAttribs.SetItemChecked(ii, true);
          }
        }
          //otherwise evaluate the string
        else
        {
          string[] DHElements = Text.Split(',');

          for (int jj = 0; jj < DHElements.Length; jj++)
          {
            if (lstDHAttribs.Items.Contains(DHElements[jj].Trim()))
            {
              int loc = lstDHAttribs.Items.IndexOf(DHElements[jj].Trim());
              lstDHAttribs.SetItemChecked(loc, true);
            }
          }
        }
      }
    }

    private delegate void dlstAxisAttribs_Update(string Text);
    private void lstAxisAttribs_Update(string Text)
    {
      if (lstAxisAttribs.InvokeRequired)
        lstAxisAttribs.BeginInvoke(new dlstAxisAttribs_Update(lstAxisAttribs_Update), new object[] { Text });
      else
      {
        //Clear them all to start
        for (int ii = 0; ii < lstAxisAttribs.Items.Count; ii++)
          lstAxisAttribs.SetItemChecked(ii, false);
        //if there is something wrong do this...
        if (Text == null || Text.ToLower() == "all")
        {
          for (int ii = 0; ii < lstAxisAttribs.Items.Count; ii++)
          {
            lstAxisAttribs.SetItemChecked(ii, true);
          }
        }
        //otherwise evaluate the string
        else
        {
          string[] AxisElements = Text.Split(',');
          for (int jj = 0; jj < AxisElements.Length; jj++)
          {
            if (lstAxisAttribs_Contains(AxisElements[jj].Trim()))
            {
              int loc = lstAxisAttribs_IndexOf(AxisElements[jj].Trim());
              lstAxisAttribs.SetItemChecked(loc, true);
            }
          }
        }
      }
    }

    private delegate void dtxtStepSize_Text(string Text);
    private void txtStepSize_Text(string Text)
    {
      if (txtStepSize.InvokeRequired)
        this.txtStepSize.BeginInvoke(new dtxtStepSize_Text(txtStepSize_Text), new object[] { Text });
      else
        this.txtStepSize.Text = Text;
    }
    private delegate void dtxtTimeOut_Text(string Text);
    private void txtTimeOut_Text(string Text)
    {
      if( txtTimeOut.InvokeRequired)
        this.txtTimeOut.BeginInvoke(new dtxtTimeOut_Text(txtTimeOut_Text), new object[] { Text });
      else
        this.txtTimeOut.Text = Text;
    }

    private delegate void dtbMaxIterations_Text(string Text);
    private void tbMaxIterations_Text(string Text)
    {
      if (tbMaxIterations.InvokeRequired)
        this.tbMaxIterations.BeginInvoke(new dtbMaxIterations_Text(tbMaxIterations_Text), new object[] { Text });
      else
        this.tbMaxIterations.Text = Text;
    }

    void txtStepSize_MouseLeave(object sender, System.EventArgs e)
    {
      double dog = double.TryParse(txtStepSize.Text, out dog) ? dog : .00001;
      if (dog > 1.0 || dog < 0.0)
        txtStepSize.Text = ".00001"; //default value.
    }
    
    private bool lstAxisAttribs_Contains(string item)
    {
      for (int ii = 0; ii < lstAxisAttribs.Items.Count; ii++)
      {
        if (((cAxisAttrib)lstAxisAttribs.Items[ii]).ToString() == item) 
          return true;
      }
      return false;
    }

    private int lstAxisAttribs_IndexOf(string item)
    {
      for (int ii = 0; ii < lstAxisAttribs.Items.Count; ii++)
      {
        if (((cAxisAttrib)lstAxisAttribs.Items[ii]).ToString() == item)
          return ii;
      }
      return -1;
    }

    private void chkSolveXTable_CheckedChanged(object sender, EventArgs e)
		{
			EvaluateTableSolnStatus();
		}

		private void chkSolveYTable_CheckedChanged(object sender, EventArgs e)
		{
			EvaluateTableSolnStatus();
		}

		private void chkSolveZTable_CheckedChanged(object sender, EventArgs e)
		{
			EvaluateTableSolnStatus();
		}

		private void btnSaveData_Click(object sender, EventArgs e)
		{
			//string FileOut = (this.lblFileConfig.Text);
			//this.myMachine.ToFile(FileOut);

      this.btnSolveOnce_Click(null, null);

      if (myPoints == null || myPoints.Count <= 0)
      {
        System.Windows.Forms.MessageBox.Show("No points to save!");
        return;
      }
      
      string FileOut = this.lblFileData.Text;
			FileOut = FileOut.Substring(0, FileOut.Length - 4);
			FileOut += "out.xml";

			using (XmlTextWriter x = new XmlTextWriter(FileOut, System.Text.Encoding.UTF8))
			{
				x.Formatting = Formatting.Indented;
				x.WriteStartDocument();
				x.WriteStartElement("Electroimpact_SolverResultToolPoints");
				for (int ii = 0; ii < myPoints.Count; ii++)
				{
					x.WriteStartElement("Data");
					x.WriteAttributeString("Xtp", ((cPoint)myPoints[ii]).Xfwd.ToString("F4"));
					x.WriteAttributeString("Ytp", ((cPoint)myPoints[ii]).Yfwd.ToString("F4"));
					x.WriteAttributeString("Ztp", ((cPoint)myPoints[ii]).Zfwd.ToString("F4"));
					x.WriteAttributeString("Atp", ((cPoint)myPoints[ii]).Afwd.ToString("F4"));
					x.WriteAttributeString("Btp", ((cPoint)myPoints[ii]).Bfwd.ToString("F4"));
					x.WriteAttributeString("Ctp", ((cPoint)myPoints[ii]).Cfwd.ToString("F4"));
					x.WriteEndElement();
				}
				x.WriteEndElement();
				x.Flush();
				x.Close();
			}
		}

		private void btnSaveConfig_Click(object sender, EventArgs e)
		{
      if (myMachine == null || !myMachine.IsHookedUp)
      {
        MessageBox.Show("Machine is null or not hooked up!");
        return;
      }
			string FileOut = (this.lblFileConfig.Text);

      if(FileOut.Contains("nominal"))
      {
        MessageBox.Show("Can't Save Over Nominal Config.  Check Config File Name");
        return;
      }
     

			this.myMachine.ToFile(FileOut, false,  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
		}

		private void SetFileHistory()
		{
      string file = MikesXmlSerializer.generateDefaultFilename("Electroimpact", "Solver", "files.xml");
      cFileHistory fh = new cFileHistory();
      fh.NominalConfig = this.lblFileConfigNom.Text;
      fh.Config = this.lblFileConfig.Text;
      fh.Data = this.lblFileData.Text;
      fh.SolverRoutine = this.lblFileSolver.Text;
      fh.GeneratedSolverRoutine = this.lblFileGenerated.Text;
      fh.AddlConfigFile = this.lblFileAdditional.Text;
      MikesXmlSerializer.Save(fh, file);
		}


		private void EvaluateTableSolnStatus()
		{
      if (chkSolveCompTables.Checked)
        this.grpCompTable.Enabled = true;
      else
        this.grpCompTable.Enabled = false;
		}

    private void chkSolveCompTables_CheckedChanged(object sender, EventArgs e)
    {
      EvaluateTableSolnStatus();
    }

    private void lstCompTables_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lstCompTables.SelectedIndex >= 0)
      {
        cAxisLimits al = (cAxisLimits)lstCompTables.Items[lstCompTables.SelectedIndex];
        this.myTips.SetToolTip(lstCompTables, al.GetLimitString(myPoints));
        this.txtCompAxisLimits.Text = al.GetLimitString(myPoints);
      }
    }

    private void chkCompUseLimits_CheckedChanged(object sender, EventArgs e)
    {
      this.txtCompAxisLimits.Enabled = chkCompUseLimits.Checked;
      this.btnCompChangeLimits.Enabled = chkCompUseLimits.Checked;
    }

    private void btnCompChangeLimits_Click(object sender, EventArgs e)
    {
      if (lstCompTables.SelectedIndex >= 0)
      {
        cAxisLimits a = (cAxisLimits)lstCompTables.Items[lstCompTables.SelectedIndex];
        a.ChangeAxisLimits(txtCompAxisLimits.Text);
        lstCompTables.Items[lstCompTables.SelectedIndex] = a;
      }
    }

    private void btnNullCompTables_Click(object sender, EventArgs e)
    {
      myMachine.NullCompTables();
    }

    #endregion

    private void btnRunSolverFile_Click(object sender, EventArgs e)
    {
      string[] tstringarray;
      string tstring;
      this.CurrentTry = null;
      this.myMachine = null;
      if (!System.IO.File.Exists(this.lblFileConfig.Text))
        return;
      if (Robot(this.lblFileConfig.Text))
        this.myMachine = new Electroimpact.Machine.cRobot(this.lblFileConfig.Text, true);
      else
        this.myMachine = new Electroimpact.Machine.cMachine(this.lblFileConfig.Text, true);

      if (myPoints != null)
        myPoints.Clear();

      if (!System.IO.File.Exists(this.lblFileData.Text))
        return;

      myPoints = new cPoints(this.lblFileData.Text, myMachine.GetAxisNames());

      if (!System.IO.File.Exists(this.lblFileSolver.Text))
        return;

      XmlNodeReader r;
      XmlDocument doc;
      try
      {
        doc = new XmlDocument();
        doc.Load(this.lblFileSolver.Text);
        r = new XmlNodeReader(doc);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      if( mySolverSequence.Count > 0 )
        mySolverSequence.Clear();
      while (r.Read())
      {
        if (r.NodeType == XmlNodeType.Element)
        {
          if (r.Name == "Solver_File")
          {
          }
          if (r.Name == "Try")
          {
            cSolverSequencer Try = new cSolverSequencer();
            Try.Attempts = int.TryParse(r.GetAttribute("Attempts"), out Try.Attempts) ? Try.Attempts : 100;
            
            Try.Range = r.GetAttribute("DataRange") == null ? "all" : r.GetAttribute("DataRange");
            tstring = Try.Range.Replace(' ', ',');
            tstringarray = tstring.Split(',');
            Try.Range = "";
            for (int jj = 0; jj < tstringarray.Length; jj++)
            {
              if (cRanges.ContainsKey(tstringarray[jj]))
              {
                tstringarray[jj] = cRanges[tstringarray[jj]];
              }
              Try.Range += tstringarray[jj] + " ";
            }
            Try.Range = Try.Range.TrimEnd(null);
            Try.SetEngineType(r.GetAttribute("ProblemType"));
            Try.UseLimits = r.GetAttribute("UseLimits") == null ? false : r.GetAttribute("UseLimits").ToLower() == "true";
            Try.TimeOut = r.GetAttribute("TimeOut") != null && int.TryParse(r.GetAttribute("TimeOut").ToLower(), out Try.TimeOut) ? Try.TimeOut : 120;
            txtTimeOut.Text = Try.TimeOut.ToString();
            string minimize = r.GetAttribute("Minimize") == null ? "all" : r.GetAttribute("Minimize");
            Try.StepSize(r.GetAttribute("StepSize"));
            if (minimize.ToLower() == "all")
            {
              Try.bXerr = true;
              Try.bYerr = true;
              Try.bZerr = true;
            }
            else
            {
              string[] arminimize = minimize.Split(',');
              for (int ii = 0; ii < arminimize.Length; ii++)
              {
                switch (arminimize[ii].ToLower().Trim())
                {
                  case "xerr":
                    Try.bXerr = true;
                    break;
                  case "yerr":
                    Try.bYerr = true;
                    break;
                  case "zerr":
                    Try.bZerr = true;
                    break;
                  default:
                    break;
                }
              }
            }
            string method = r.GetAttribute("Method") == null ? "default" : r.GetAttribute("Method");
            switch (method.ToLower())
            {
              case "default":
              case "sumsquares":
                Try.Method = cSolverSequencer.eMethod.SqrtSumSq;
                break;
              case "flatten":
                Try.Method = cSolverSequencer.eMethod.Flatten;
                break;
              case "experimental":
                Try.Method = cSolverSequencer.eMethod.Experimental;
                break;
              default:
                opnSumSquares_CheckedChanged(true);
                break;
            }

            while (r.Read())
            {
              if (r.NodeType == XmlNodeType.EndElement)
              {
                if (r.Name == "Try")
                {
                  mySolverSequence.Add(Try);
                  break;
                }
              }
              if (r.NodeType == XmlNodeType.Element)
              {
                if (r.Name == "CompTable")
                {
                  Try.chkCompTables = true;
                  Try.CompTable.TableString = r.GetAttribute("TableString") == null ? "all" : r.GetAttribute("TableString"); 
                  string Elements = r.GetAttribute("Elements");
                  if (Elements == null || Elements.ToLower() == "all")
                  {
                    Try.CompTable.bX = true;
                    Try.CompTable.bY = true;
                    Try.CompTable.bZ = true;
                    Try.CompTable.brX = true;
                    Try.CompTable.brY = true;
                    Try.CompTable.brZ = true;
                  }
                  else
                  {
                    string[] test = Elements.Split(',');
                    for (int ii = 0; ii < test.Length; ii++)
                    {
                      switch (test[ii])
                      {
                        case "X":
                          Try.CompTable.bX = true;
                          break;
                        case "Y":
                          Try.CompTable.bY = true;
                          break;
                        case "Z":
                          Try.CompTable.bZ = true;
                          break;
                        case "rX":
                          Try.CompTable.brX = true;
                          break;
                        case "rY":
                          Try.CompTable.brY = true;
                          break;
                        case "rZ":
                          Try.CompTable.brZ = true;
                          break;
                      }
                    }
                  }
                }
                else if (r.Name == "DHAttributes")
                {
                  Try.chkDHAttributes = true;
                  Try.DHAttributes = r.GetAttribute("Elements");
                }
                else if (r.Name == "AxisAttributes")
                {
                  Try.chkAxisAttributes = true;
                  Try.AxisAttributes = r.GetAttribute("Elements");
                }
                else if (r.Name.ToLower() == "programfunction")
                {
                  Try.ProgramFunction = r.GetAttribute("Instruction");
                }
              }
            }
          }
        }
      }
      r.Close();

      SolverThread = new System.Threading.Thread(new System.Threading.ThreadStart(SolverSequencer));
      SolverThread.Name = "WorkerThread";
      SolverThread.Start();
      
    }

    private System.Collections.Generic.List<cSolverSequencer> mySolverSequence = new List<cSolverSequencer>();

    private class cSolverSequencer
    {
      public bool bXerr = false;
      public bool bYerr = false;
      public bool bZerr = false;
      public bool chkCompTables = false;
      public bool chkDHAttributes = false;
      public bool chkAxisAttributes = false;
      public int Attempts = 100;
      public string Range = "all";
      public cTryCompTable CompTable = new cTryCompTable();
      public string DHAttributes = "";
      public string AxisAttributes = "";
      public string ProgramFunction = "";
      public bool UseLimits = true;
      public int TimeOut = 120;  //units of seconds.
      private double stepsize = .00001;

      public enum eEngineType
      {
        Linear = 0, //Seems to be more accurate
        NonLinear,   //Seems less prone to get stuck in local minimums
        Evolutionary //Seems to piss off conservatives
      }
      public eEngineType EngineType = eEngineType.Linear;
      public enum eMethod
      {
        SqrtSumSq = 0,
        Flatten,
        Experimental
      };
      public eMethod Method = eMethod.SqrtSumSq;

      public void SetEngineType( string sEngineType )
      {
        EngineType = eEngineType.Linear;
        if (sEngineType == null)
          return;
        switch (sEngineType.ToLower())
        {
          case "linear":
            EngineType = eEngineType.Linear;
            break;
          case "nonlinear":
            EngineType = eEngineType.NonLinear;
            break;
          case "evolutionary":
            EngineType = eEngineType.Evolutionary;
            break;
          default:
            EngineType = eEngineType.Linear;
            break;
        }
      }
      public double StepSize(string stringStepSize)
      {
        if (stringStepSize == null)
          stepsize = .00001;
        stepsize = double.TryParse(stringStepSize, out stepsize) ? stepsize : .00001;
        return stepsize;
      }
      public double StepSize()
      {
        return stepsize;
      }
    }

    private class cTryCompTable
    {
      public bool bX = false;
      public bool bY = false;
      public bool bZ = false;
      public bool brX = false;
      public bool brY = false;
      public bool brZ = false;
      public string TableString = "all";
    }

    private delegate void dbtnSolveIt_PerformClick();
    private void btnSolveIt_PerformClick()
    {
      if (btnSolveIt.InvokeRequired)
        btnSolveIt.BeginInvoke(new dbtnSolveIt_PerformClick(btnSolveIt_PerformClick));
      else
        btnSolveIt.PerformClick();
    }

    private delegate void dbtnSaveAll_PerformClick();
    private void btnSaveAll_PerformClick()
    {
      if (btnSaveAll.InvokeRequired)
        btnSaveAll.BeginInvoke(new dbtnSaveAll_PerformClick(btnSaveAll_PerformClick));
      else
        btnSaveAll.PerformClick();
    }
    private void SolverSequencer()
    {
      btnSaveResults_Click(null, null);
      for (int ii = 0; ii < mySolverSequence.Count; ii++)
      {

        //lblSeqStepValue.Text = ii.ToString();
        

        this.chk_lblSeqStepValue((ii+1).ToString() + " of " + mySolverSequence.Count.ToString());



        cSolverSequencer Try = mySolverSequence[ii];
        this.CurrentTry = Try;

        txtRange_Text(Try.Range);

        System.Threading.Monitor.Enter(myError);
        this.myError.count =  Try.Attempts;
        pgStatus_Maximum(this.myError.count);
        System.Threading.Monitor.Exit(myError);
        chkXerr_Checked(Try.bXerr);
        chkYerr_Checked(Try.bYerr);
        chkZerr_Checked(Try.bZerr);

        chkCompUseLimits_Checked(Try.UseLimits);

        opnSumSquares_CheckedChanged(Try.Method == cSolverSequencer.eMethod.SqrtSumSq);
        opnExperimental_CheckedChanged(Try.Method == cSolverSequencer.eMethod.Experimental);


        chkSolveDHLinks_Checked(Try.chkDHAttributes);
        lstDHAttribs_Update(Try.DHAttributes);

        chkSolveAxAttribs_Checked(Try.chkAxisAttributes);
        lstAxisAttribs_Update(Try.AxisAttributes);

        chkSolveCompTables_Checked(Try.chkCompTables);

        chkCTX_Checked(Try.CompTable.bX);
        chkCTY_Checked(Try.CompTable.bY);
        chkCTZ_Checked(Try.CompTable.bZ);
        chkCTRoll_Checked(Try.CompTable.brX);
        chkCTPitch_Checked(Try.CompTable.brY);
        chkCTYaw_Checked(Try.CompTable.brZ);
        lstCompTables_Update(Try.CompTable.TableString);
        txtStepSize_Text(Try.StepSize().ToString());
        txtTimeOut_Text(Try.TimeOut.ToString());
        tbMaxIterations_Text(Try.Attempts.ToString());


        if (chkDebugMode.Checked)
        {
          System.Threading.Thread.Sleep(2000);
          myError.Abort = false;
        }
        else
        {
          System.Threading.Thread.Sleep(500); //Give the UI a chance to update.
          SolveMe();
        }

        btnSaveResults_Click(null, null);

        if (Try.ProgramFunction.ToLower() == "pause")
        {
          //while (!myError.Abort)
          myError.Pause = true; //start the pause
          while(myError.Pause)  //pause must be ended by "unpause" button
            System.Threading.Thread.Sleep(20);
          myError.Abort = false;
          myError.Pause = false;
        }
        else if (Try.ProgramFunction.ToLower() == "quit")
          break;
        else if (myError.Abort)
          break;
      } // end for (int ii = 0; ii < mySolverSequence.Count; ii++)
    }

    private void btnDH_checkall_Click(object sender, EventArgs e)
    {
      //select all DH links
      for (int ii = 0; ii < this.lstDHAttribs.Items.Count; ii++)
      {
        this.lstDHAttribs.SetItemChecked(ii, true);
      }
    }

    private void btnDH_uncheckall_Click(object sender, EventArgs e)
    {
      //select all DH links
      for (int ii = 0; ii < this.lstDHAttribs.Items.Count; ii++)
      {
        this.lstDHAttribs.SetItemChecked(ii, false);
      }
    }

    private void btnAxAt_checkall_Click(object sender, EventArgs e)
    {
      //select all axis attributes
      for (int ii = 0; ii < this.lstAxisAttribs.Items.Count; ii++)
      {
        this.lstAxisAttribs.SetItemChecked(ii, true);
      }      
    }

    private void btnAxAt_uncheckall_Click(object sender, EventArgs e)
    {
      //select all axis attributes
      for (int ii = 0; ii < this.lstAxisAttribs.Items.Count; ii++)
      {
        this.lstAxisAttribs.SetItemChecked(ii, false);
      }  
    }

    private void btnWriteSeq_Click(object sender, EventArgs e)
    {


      //generate a new file in the same directory as the config file
      if (!System.IO.File.Exists(this.lblFileConfig.Text))
      {
        MessageBox.Show("Error: Need to set a config File");
        return;
      }



      //get just the filename without directory
      string[] temp_ar = this.lblFileConfig.Text.Split('\\');
      string temp = temp_ar[temp_ar.Length - 1]; //the file name without directory
      string FileOut = "";
      string FileOutTemp = "";

      //subtract the file name to get just the directory
      string DirConfig = this.lblFileConfig.Text.Replace(temp, "");

      string sDateTime = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
      sDateTime += "_" + DateTime.Now.Hour.ToString() + "hr" + DateTime.Now.Minute.ToString("00") + "min";


      
      //put the output file in the same directory as the config file
      lblFileGenerated.Text = DirConfig + "Generated_" + sDateTime + ".solver.routine.xml";


      using (XmlTextWriter x = new XmlTextWriter(lblFileGenerated.Text, System.Text.Encoding.UTF8))
      {
        x.Formatting = Formatting.Indented;
        x.WriteStartDocument();
        x.WriteStartElement("Solver_File");
        x.WriteStartElement("Trys");
        x.WriteComment("");
        x.WriteEndElement();
        x.WriteEndElement();
        x.Close();

      }

      SetFileHistory(); //save file names

    }

    private void mnuArchiveConfig_Click(object sender, EventArgs e)
    {
      //read the config file and write it out to a new file with date code

      if (!System.IO.File.Exists(this.lblFileConfig.Text))
        return;


      string ArchiveFile = this.lblFileConfig.Text;
      string sDateTime = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
      sDateTime += "_" + DateTime.Now.Hour.ToString() + "hr" + DateTime.Now.Minute.ToString("00") + "min";
      sDateTime += ".config";


      ArchiveFile = ArchiveFile.Replace("config", sDateTime);


      System.IO.File.Copy(this.lblFileConfig.Text, ArchiveFile);

    }

    private bool SetRanges()
    {

      this.myRanges.Clear();
      if (this.txtRange.Text.ToUpper() == "ALL")
      {
        int Start = 0;
        int End = this.myPoints.Count - 1;
        this.myRanges.Add(Start);
        this.myRanges.Add(End);
      }
      else
      {
        string[] ranges = this.txtRange.Text.Split(' ');
        int Start, End;

        for (int ii = 0; ii < ranges.Length; ii++)
        {
          string[] range;

          //check if ranges2[ii] is a symbol defined by *.data.xml
          if (cRanges.ContainsKey(ranges[ii]))
          {
            //get symbol range defined by *.data.xml
            string range_of_symbol = cRanges[ranges[ii]];
            string[] ranget;
            //resolve symbol into a range seperated by '-' or ' '
            ranget = range_of_symbol.Split(' ');
            string new_range = "";

            for (int jj = 0; jj < ranget.Length; jj++)
            {
              if (!ranget[jj].Contains("-"))
                ranget[jj] += "-" + ranget[jj];
              if (jj == 0)
                new_range += ranget[jj];
              else
                new_range += " " + ranget[jj];
            }

            range = new_range.Split('-',' ');
          }
          else
          {
            range = ranges[ii].Split('-');
          }


          if (range.Length == 1) //for single digits
          {

            if (!int.TryParse(range[0], out Start))
            {
              MessageBox.Show("Range field is shitty");
              return false;
            }
            End = Start;

            this.myRanges.Add(Start);
            this.myRanges.Add(End);
          }
          else //for range eg. 24-96, get Start != End
          {

            if (range.Length % 2 != 0)
            {
              MessageBox.Show("Range field is shitty");
              return false;
            }

            for (int j = 0; j < range.Length; j = j + 2)
            {
              if (!int.TryParse(range[j], out Start))
              {
                MessageBox.Show("Range field is shitty");
                return false;
              }
              if (!int.TryParse(range[j + 1], out End))
              {
                MessageBox.Show("Range field is shitty");
                return false;
              }
              if (Start > End)
              {
                MessageBox.Show("Range field is shitty");
                return false;
              }
              this.myRanges.Add(Start);
              this.myRanges.Add(End);
            } 
          }
        }
      }

      if (this.myRanges.Count == 0)
      {
        MessageBox.Show("Range field is shitty");
        return false;
      }

      return true; //make it here if range is ok
    }

    private void BtnNextStep_Click(object sender, EventArgs e)
    {





      string FileOut = "";
      string FileOutTemp = "";

      FileOut = lblFileGenerated.Text;

      if (!System.IO.File.Exists(FileOut))
      {
        MessageBox.Show("Error: There is no Pre-existing Generated File");
        return;
      }


      //get just the filename without directory
      string[] temp_ar = this.lblFileGenerated.Text.Split('\\');
      string temp = temp_ar[temp_ar.Length - 1]; //the file name without directory


      //subtract the file name to get just the directory
      string DirConfig = this.lblFileGenerated.Text.Replace(temp, "");
      FileOutTemp = DirConfig + "Generated_Temp.solver.routine.xml";

      #region Evaluating Range String

      bool RangeOK = SetRanges();

      if (!RangeOK)
        return;


      #endregion

      #region Evaluating Items to solve

      #region Links
      LinkKeys.Clear();
      if (this.chkSolveDHLinks.Checked)
      {
        for (int ii = 0; ii < this.lstDHAttribs.Items.Count; ii++)
        {
          if (this.lstDHAttribs.GetItemChecked(ii))
          {
            LinkKeys.Add(this.lstDHAttribs.Items[ii].ToString());
          }
        }
      }
      #endregion

      #region Axes
      AxisKeys.Clear();
      if (this.chkSolveAxAttribs.Checked)
      {
        for (int ii = 0; ii < this.lstAxisAttribs.Items.Count; ii++)
        {
          if (this.lstAxisAttribs.GetItemChecked(ii))
            AxisKeys.Add((cAxisAttrib)lstAxisAttribs.Items[ii]);
        }
      }
      #endregion

      int i_compTableItems = 0;

      if (this.chkSolveCompTables.Checked)
      {
        for (int ii = 0; ii < lstCompTables.Items.Count; ii++)
        {
          if (this.lstCompTables.GetItemChecked(ii))
            i_compTableItems += 1;
        }
      }

      if (!((this.chkSolveDHLinks.Checked && LinkKeys.Count > 0) || (this.chkSolveAxAttribs.Checked && AxisKeys.Count > 0) || (this.chkSolveCompTables.Checked && i_compTableItems > 0)))
      {
        MessageBox.Show("Error: You must choose something to solve");
        return;
      }


      #endregion



      //create FileOutTemp
      //read in existing xml FileOut
      //as read in each line write it out to temp file
      //when hit /Trys write out new lines
      //when finished delete FileOut and copy FileOutTemp to FileOut


      using (XmlTextWriter x = new XmlTextWriter(FileOutTemp, System.Text.Encoding.UTF8))
      {

        XmlNodeReader r;
        XmlDocument document;
        bool rtn = true;
        if (File.Exists(FileOut))
        {
          try
          {
            document = new XmlDocument();
            document.Load(FileOut);
            r = new XmlNodeReader(document);
          }
          catch (Exception ex)
          {
            throw (ex);
          }
          while (r.Read())
          {
            switch (r.NodeType)
            {

              case XmlNodeType.XmlDeclaration:
                x.Formatting = Formatting.Indented;
                x.WriteStartDocument();
                break;
              case XmlNodeType.Element:

                if (r.Name == "Try")
                {

                  int attempts = int.TryParse(r.GetAttribute("Attempts"), out attempts) ? attempts : 100;
                  string range = r.GetAttribute("DataRange") == null ? "all" : r.GetAttribute("DataRange");
                  string uselimits = r.GetAttribute("UseLimits") == null ? "false" : r.GetAttribute("UseLimits").ToLower();
                  int timeout = r.GetAttribute("TimeOut") != null && int.TryParse(r.GetAttribute("TimeOut").ToLower(), out timeout) ? timeout : 120;
                  string minimize = r.GetAttribute("Minimize") == null ? "all" : r.GetAttribute("Minimize");
                  string method = r.GetAttribute("Method") == null ? "default" : r.GetAttribute("Method");



                  x.WriteStartElement("Try");
                  //do this for each attribute:
                  x.WriteAttributeString("DataRange", range); //have to get all of them as if press the solve button
                  x.WriteAttributeString("Attempts", attempts.ToString()); //tbMaxIterations.Text
                  x.WriteAttributeString("Minimize", minimize);
                  x.WriteAttributeString("ProblemType", r.GetAttribute("ProblemType"));
                  x.WriteAttributeString("UseLimits", uselimits);
                  x.WriteAttributeString("StepSize", r.GetAttribute("StepSize"));
                  x.WriteAttributeString("TimeOut", timeout.ToString());
                  x.WriteAttributeString("Method", method);



                  bool exit_try = false;

                  while (!exit_try)
                  {
                    r.Read();

                    if (r.NodeType == XmlNodeType.EndElement)
                    {
                      if (r.Name == "Try")
                      {
                        x.WriteEndElement(); //end try
                        exit_try = true;
                        //break;
                      }
                    }
                    else
                    {
                      if (r.Name != "Try")
                      {
                        if (r.Name == "CompTable")
                        {
                          string tablestring = r.GetAttribute("TableString") == null ? "all" : r.GetAttribute("TableString");
                          x.WriteStartElement("CompTable");
                          x.WriteAttributeString("Elements", r.GetAttribute("Elements"));
                          x.WriteAttributeString("TableString", tablestring);
                          x.WriteEndElement();
                        }
                        else if (r.Name == "DHAttributes")
                        {
                          x.WriteStartElement("DHAttributes");
                          x.WriteAttributeString("Elements", r.GetAttribute("Elements"));
                          x.WriteEndElement();
                        }
                        else if (r.Name == "AxisAttributes")
                        {
                          x.WriteStartElement("AxisAttributes");
                          x.WriteAttributeString("Elements", r.GetAttribute("Elements"));
                          x.WriteEndElement();
                        }
                        else if (r.Name.ToLower() == "programfunction")
                        {
                          //get program instruction
                          x.WriteStartElement("ProgramFunction");
                          x.WriteAttributeString("Instruction", r.GetAttribute("Instruction"));
                          x.WriteEndElement();
                        }
                      }
                    }
                  }
                }
                else
                {
                  x.WriteStartElement(r.Name); // start element for Solver_File and Trys
                }
                break;
              case XmlNodeType.Attribute:
                x.WriteAttributeString(r.Name, r.Value);
                //x.WriteAttributeString(r.Name,r.ReadString);
                break;
              case XmlNodeType.Comment:
                x.WriteComment(r.Value);
                break;
              case XmlNodeType.EndElement:
                if (r.Name == "Trys")
                {
                  //then write out new solver data here
                  string outRanges = "";
                  string outMinimize = "All";
                  string outProblemType = "Linear";
                  string outUseLimits = "false";
                  string outMethod = "sumsquares";

                  //get string for ranges
                  for (int i_out = 0; i_out < this.myRanges.Count; i_out++)
                  {

                    outRanges = this.txtRange.Text;

                    //if (i_out == 0)
                    //  outRanges = outRanges + this.myRanges[i_out].ToString();
                    //else if (i_out % 2 == 0)
                    //  outRanges = outRanges + " " + this.myRanges[i_out].ToString();
                    //else
                    //  outRanges = outRanges + "-" + this.myRanges[i_out].ToString();
                  }

                  //get string for minimize
                  if (chkXscaler.Checked && chkYscaler.Checked && chkZscaler.Checked)
                    outMinimize = "All";
                  else
                  {
                    if (chkXscaler.Checked)
                    {
                      outMinimize = outMinimize + "Xerr";
                      if (chkYscaler.Checked)
                        outMinimize = outMinimize + ", Yerr";
                      if (chkZscaler.Checked)
                        outMinimize = outMinimize + ", Zerr";
                    }
                    else if (chkYscaler.Checked)
                    {
                      outMinimize = outMinimize + "Yerr";
                      if (chkZscaler.Checked)
                        outMinimize = outMinimize + ", Zerr";
                    }
                    else if (chkZscaler.Checked)
                      outMinimize = outMinimize + "Zerr";
                  }

                  //get string for ProblemType
                  if (radioLinear.Checked)
                    outProblemType = "Linear";
                  else if (radioNonLinear.Checked)
                    outProblemType = "NonLinear";

                  //get string for UseLimits
                  if (chkCompUseLimits.Checked && chkSolveCompTables.Checked)
                    outUseLimits = "true";
                  else
                    outUseLimits = "false";

                  //get string for Method
                  if (opnExperimental.Checked)
                    outMethod = "experimental";
                  else
                    outMethod = "sumsquares";


                  x.WriteStartElement("Try");
                  //do this for each attribute:
                  x.WriteAttributeString("DataRange", outRanges); //have to get all of them as if press the solve button
                  x.WriteAttributeString("Attempts", tbMaxIterations.Text); //tbMaxIterations.Text
                  x.WriteAttributeString("Minimize", outMinimize);
                  x.WriteAttributeString("ProblemType", outProblemType);
                  x.WriteAttributeString("UseLimits", outUseLimits);
                  x.WriteAttributeString("StepSize", txtStepSize.Text);
                  x.WriteAttributeString("TimeOut", txtTimeOut.Text);
                  x.WriteAttributeString("Method", outMethod);


                  //get DH links:
                  if (chkSolveDHLinks.Checked)
                  {
                    string outLinks = "";

                    for (int i_links = 0; i_links < LinkKeys.Count; i_links++)
                    {
                      if (i_links == 0)
                        outLinks = outLinks + LinkKeys[i_links].ToString();
                      else
                        outLinks = outLinks + "," + LinkKeys[i_links].ToString();
                    }

                    x.WriteStartElement("DHAttributes");
                    x.WriteAttributeString("Elements", outLinks);
                    x.WriteEndElement();
                  }

                  //get axis attributes
                  if (chkSolveAxAttribs.Checked)
                  {
                    string outAxAttrib = "";

                    for (int i_axAt = 0; i_axAt < AxisKeys.Count; i_axAt++)
                    {
                      if (i_axAt == 0)
                        outAxAttrib = outAxAttrib + AxisKeys[i_axAt].ToString();
                      else
                        outAxAttrib = outAxAttrib + "," + AxisKeys[i_axAt].ToString();
                    }

                    x.WriteStartElement("AxisAttributes");
                    x.WriteAttributeString("Elements", outAxAttrib);
                    x.WriteEndElement();
                  }

                  //get comp tables
                  if (chkSolveCompTables.Checked)
                  {
                    string outCompTbl = "";
                    int CompStationsCount = 0;

                    for (int ii = 0; ii < lstCompTables.Items.Count; ii++)
                    {

                      if (lstCompTables.GetItemChecked(ii))
                      {

                        cAxisLimits ax = (cAxisLimits)lstCompTables.Items[ii];
                        string outAxCmp = ax.AxisName.ToString();
                        string sCompAxisLimits = "";// = txtCompAxisLimits.Text;
                        string outAxElmt = "";


                        if (chkCompUseLimits.Checked)
                        {
                          double minusLim = ax.MinusLimit;
                          double plusLim = ax.PlusLimit;
                          sCompAxisLimits = minusLim.ToString("F3") + " to " + plusLim.ToString("F3");
                          outAxCmp = outAxCmp + " " + sCompAxisLimits;
                        }


                        if (chkCTX.Checked)
                          outAxElmt = ",X";
                        if (chkCTY.Checked)
                          outAxElmt = outAxElmt + ",Y";
                        if (chkCTZ.Checked)
                          outAxElmt = outAxElmt + ",Z";
                        if (chkCTRoll.Checked)
                          outAxElmt = outAxElmt + ",rX";
                        if (chkCTPitch.Checked)
                          outAxElmt = outAxElmt + ",rY";
                        if (chkCTYaw.Checked)
                          outAxElmt = outAxElmt + ",rZ";

                        outAxElmt = outAxElmt.Substring(1, (outAxElmt.Length - 1));



                        x.WriteStartElement("CompTable");
                        x.WriteAttributeString("Elements", outAxElmt);
                        x.WriteAttributeString("TableString", outAxCmp);
                        x.WriteEndElement();
                      }
                    }
                  }

                  //get program instruction
                  x.WriteStartElement("ProgramFunction");
                  x.WriteAttributeString("Instruction", "");
                  x.WriteEndElement();

                  x.WriteEndElement(); //end trys
                  //x.WriteEndElement(); //end trys
                  //x.WriteEndElement(); //end Solver_File

                  break;
                }
                else
                  x.WriteEndElement();
                break;

              default:
                break;
            }
          }
          r.Close();
        } //end of add to existing file

        x.WriteEndElement(); // end solver_file

        x.Flush();
        x.Close();



        //delete original file
        System.IO.File.Delete(FileOut);

        //copy temp file to final file
        System.IO.File.Copy(FileOutTemp, FileOut);

        //delete temp file
        System.IO.File.Delete(FileOutTemp);

      }
    }

    private void mnuSetGenerated_Click(object sender, EventArgs e)
    {
      System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();
      if (System.IO.File.Exists(this.lblFileGenerated.Text))
      {
        dlg.FileName = this.lblFileGenerated.Text;
      }
      else
      {
        Electroimpact.FileIO.cFileOther f = new Electroimpact.FileIO.cFileOther();
        dlg.InitialDirectory = f.CurrentFolder() + "Data";
      }


      dlg.Multiselect = false;
      dlg.Filter = "Solver File|*.solver.routine.xml";
      if (dlg.ShowDialog() == DialogResult.OK)
      {


        this.lblFileGenerated.Text = dlg.FileName;
        //this.PopulateVariableBoxes();
        SetFileHistory();
      }
    }

    private void mnuSaveProject_Click(object sender, EventArgs e)
    {


      Stream myStream;
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      
      //saveFileDialog1.Filter = "xml File|*.project.xml|All files (*.*)";
      saveFileDialog1.Filter = "All files (*.*)|*.*|project files (*.proj)|*.proj";


      saveFileDialog1.FilterIndex = 2;
      saveFileDialog1.RestoreDirectory = true;
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        string file = saveFileDialog1.FileName;
        using (XmlTextWriter x = new XmlTextWriter(file, System.Text.Encoding.UTF8))
        {

          
          string configNomName = "";
          string configName = "";
          string dataName = "";
          string solverName = "";
          string generatedName = "";
          string additionalName = "";



          string projName = "";
          string DirProject = GetDirectory(saveFileDialog1.FileName, out projName);


          //get file names without directory and copy files if necessary
          configNomName = SetFileInProject(this.lblFileConfigNom.Text, DirProject);
          configName = SetFileInProject(this.lblFileConfig.Text, DirProject);
          dataName = SetFileInProject(this.lblFileData.Text, DirProject);
          solverName = SetFileInProject(this.lblFileSolver.Text, DirProject);
          generatedName = SetFileInProject(this.lblFileGenerated.Text, DirProject);
          additionalName = SetFileInProject(this.lblFileAdditional.Text, DirProject);





          x.Formatting = Formatting.Indented;
          x.WriteStartDocument();
          x.WriteStartElement("Files");
          x.WriteStartElement("NomConfig");
          //x.WriteAttributeString("value", this.lblFileConfigNom.Text);
          x.WriteAttributeString("value", configNomName);
          x.WriteEndElement();
          x.WriteStartElement("Config");
          x.WriteAttributeString("value", configName);
          x.WriteEndElement();
          x.WriteStartElement("Data");
          x.WriteAttributeString("value", dataName);
          x.WriteEndElement();
          x.WriteStartElement("SolverFile");
          x.WriteAttributeString("value", solverName);
          x.WriteEndElement();
          x.WriteStartElement("GeneratedSolverFile");
          x.WriteAttributeString("value", generatedName);
          x.WriteEndElement();
          x.WriteStartElement("AdditionalFile");
          x.WriteAttributeString("value", additionalName);
          x.WriteEndElement();
          x.WriteEndElement();
          x.Flush();
          x.Close();
        }
      }
    }

    private void mnuLoadProject_Click(object sender, EventArgs e)
    {

      String FileName = "";

      System.Windows.Forms.OpenFileDialog dlg = new OpenFileDialog();

      dlg.Multiselect = false;
      dlg.Filter = "Project File|*.proj";
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        FileName = dlg.FileName;
      }


      XmlNodeReader r;
      XmlDocument document;
      try
      {
        document = new XmlDocument();
        document.Load(FileName);
        r = new XmlNodeReader(document);

        string projName = "";
        string DirProject = GetDirectory(FileName, out projName);

        bool config = false;
        bool config_nom = false;
        bool data = false;
        bool solver = false;
        bool generated = false;
        bool additional = false;
        while (r.Read())
        {
          switch (r.NodeType)
          {
            case XmlNodeType.Element:
              {
                if (r.Name == "NomConfig")
                {
                  this.lblFileConfigNom.Text = DirProject + r.GetAttribute("value");
                  if (!System.IO.File.Exists(this.lblFileConfigNom.Text))
                    this.lblFileConfigNom.Text = "None Selected";
                  config = true;
                }
                if (r.Name == "Config")
                {
                  this.lblFileConfig.Text = DirProject + r.GetAttribute("value");
                  if (!System.IO.File.Exists(this.lblFileConfig.Text))
                    this.lblFileConfig.Text = "None Selected";
                  config_nom = true;
                }
                if (r.Name == "Data")
                {
                  this.lblFileData.Text = DirProject + r.GetAttribute("value");
                  if (!System.IO.File.Exists(this.lblFileData.Text))
                    this.lblFileData.Text = "None Selected";
                  data = true;
                }
                if (r.Name == "SolverFile")
                {
                  this.lblFileSolver.Text = DirProject + r.GetAttribute("value");
                  if (!System.IO.File.Exists(this.lblFileSolver.Text))
                    this.lblFileSolver.Text = "None Selected";
                  solver = true;
                }
                if (r.Name == "GeneratedSolverFile")
                {
                  this.lblFileGenerated.Text = DirProject + r.GetAttribute("value");
                  if (!System.IO.File.Exists(this.lblFileGenerated.Text))
                    this.lblFileGenerated.Text = "None Selected";
                  generated = true;
                }
                if (r.Name == "AdditionalFile")
                {
                  this.lblFileAdditional.Text = DirProject + r.GetAttribute("value");
                  if (!System.IO.File.Exists(this.lblFileAdditional.Text))
                    this.lblFileAdditional.Text = "None Selected";
                  additional = true;
                }
                break;
              }

          }
        }

        if (!(config && config_nom && data && solver && generated && additional))
        {
          MessageBox.Show("This is not a complete Project File");
          return;
        }

        cRanges.Clear(); //clear the named ranges so that they can be reloaded

        this.PopulateVariableBoxes();
        SetFileHistory();
      }
      catch(Exception ex )
      {
        MessageBox.Show(ex.Message);
        throw;
      }

    }

    private string GetDirectory(string fName, out string fName_only)
    {
      //get just the filename without directory
      string[] temp_ar = fName.Split('\\');
      fName_only = temp_ar[temp_ar.Length - 1]; //the file name without directory
      //subtract the file name to get just the directory
      return fName.Replace(fName_only, "");
    }

    private void ProjectFileCopy(string CopyFrom, string CopyTo)
    {
      if (!System.IO.File.Exists(CopyTo))
      {
        //file doesn't exist, copy from existing file

        //only copy if there is an existing file
        if (System.IO.File.Exists(CopyFrom))
        {
          System.IO.File.Copy(CopyFrom, CopyTo);
        }
      }
    }

    private string SetFileInProject(string FileInProj, string ProjDirectory)
    {

      string justTheFile = "";
      string nameInProjDir = "";

      //get nominal config file
      string temp = GetDirectory(FileInProj, out justTheFile);
      nameInProjDir = ProjDirectory + justTheFile;

      //if file doesn't exist in same directory as Project file then copy it to there
      ProjectFileCopy(FileInProj, nameInProjDir);

      //write out name as the file name only, no directory
      return justTheFile;
    }

    private void btnUnPause_Click(object sender, EventArgs e)
    {
      myError.Pause = false;
    }

    private void lblFileConfigNom_Click(object sender, EventArgs e)
    {
      if (System.IO.File.Exists(lblFileConfigNom.Text))
      {
        if (e.GetType() == typeof(MouseEventArgs))
        {
          MouseEventArgs me = (MouseEventArgs)e;
          string fn = lblFileConfigNom.Text;
          lblFileConfigNom.LinkVisited = true;
          LabelClickHandler(me, fn);
        }
      }
      else
        MessageBox.Show("File does not exist");
    }

    private static void LabelClickHandler(MouseEventArgs me, string fn)
    {
      if (me.Button == System.Windows.Forms.MouseButtons.Left)
      {
        System.Diagnostics.Process.Start(fn);
      }
      else if (me.Button == System.Windows.Forms.MouseButtons.Right)
      {
        System.IO.FileInfo fi = new FileInfo(fn);
        System.Diagnostics.Process.Start(fi.DirectoryName);
      }
    }

    private void lblFileConfig_Click(object sender, EventArgs e)
    {
      if (System.IO.File.Exists(lblFileConfig.Text))
      {
        if (e.GetType() == typeof(MouseEventArgs))
        {
          MouseEventArgs me = (MouseEventArgs)e;
          string fn = lblFileConfig.Text;
          lblFileConfig.LinkVisited = true;
          LabelClickHandler(me, fn);
        }
      }
      else
        MessageBox.Show("File does not exist");
    }

    private void lblFileAdditional_Click(object sender, EventArgs e)
    {
      if (System.IO.File.Exists(lblFileAdditional.Text))
        if (e.GetType() == typeof(MouseEventArgs))
        {
          MouseEventArgs me = (MouseEventArgs)e;
          string fn = lblFileAdditional.Text;
          lblFileAdditional.LinkVisited = true;
          LabelClickHandler(me, fn);
        }
        else
        MessageBox.Show("File does not exist");
    }

    private void lblFileData_Click(object sender, EventArgs e)
    {
      if (System.IO.File.Exists(lblFileData.Text))
        if (e.GetType() == typeof(MouseEventArgs))
        {
          MouseEventArgs me = (MouseEventArgs)e;
          string fn = lblFileData.Text;
          lblFileData.LinkVisited = true;
          LabelClickHandler(me, fn);
        }
        else
        MessageBox.Show("File does not exist");
    }

    private void lblFileSolver_Click(object sender, EventArgs e)
    {
      if (System.IO.File.Exists(lblFileSolver.Text))
        if (e.GetType() == typeof(MouseEventArgs))
        {
          MouseEventArgs me = (MouseEventArgs)e;
          string fn = lblFileSolver.Text;
          lblFileSolver.LinkVisited = true;
          LabelClickHandler(me, fn);
        }
        else
        MessageBox.Show("File does not exist");
    }

    private void lblFileGenerated_Click(object sender, EventArgs e)
    {
      if (System.IO.File.Exists(lblFileGenerated.Text))
        if (e.GetType() == typeof(MouseEventArgs))
        {
          MouseEventArgs me = (MouseEventArgs)e;
          string fn = lblFileGenerated.Text;
          lblFileGenerated.LinkVisited = true;
          LabelClickHandler(me, fn);
        }
        else
        MessageBox.Show("File does not exist");
    }

    private void createCLEHeaderToolStripMenuItem_Click(object sender, EventArgs e)
    {
      System.IO.FileInfo fi = new FileInfo(lblFileConfig.Text);
      string filename = fi.DirectoryName;
      filename += "\\compcfg.c"; //filename.Replace(".xml", ".c");
      System.Collections.Generic.List<string> lines = new List<string>();



      #region Beginning
      string aline = "/*configfile.c created: " + System.DateTime.Now.ToString();
      lines.Add(aline);
      aline = "Automatically generated file based on " + fi.FullName + " output from the solver app.*/";
      lines.Add(aline);
      lines.Add("\n\n");
      for (int ii = 0; ii < myMachine.GetAttributeNames().Length; ii++)
      {
        string attribName = myMachine.GetAttributeNames()[ii];
        lines.Add("double " + attribName + ";");
      }
      lines.Add("\n");
      for (int ii = 0; ii < myMachine.GetAxisNames(false).Length; ii++)
      {


        string axName = myMachine.GetAxisNames(false)[ii];
        string table_var_name = axName + "_Table";
        int count = myMachine.GetCompensationTable(axName).Count;
        lines.Add("long " + table_var_name + "_Count;");
      }

      lines.Add("\n");

      for (int ii = 0; ii < myMachine.GetAxisNames(false).Length; ii++)
      {
        string axis_name = myMachine.GetAxisNames(false)[ii];
        string attrib_name = "scalefactor";
        double attrib_value = myMachine.ReadAxisAttribute(axis_name, attrib_name);
        lines.Add("double " + axis_name + "_" + attrib_name + ";");
      }

      lines.Add("\n");

      for (int ii = 0; ii < myMachine.GetAxisNames(false).Length; ii++)
      {
        string axName = myMachine.GetAxisNames(false)[ii];
        string table_var_name = axName + "_Table";
        System.Collections.Generic.List<Electroimpact.Machine.cCompTable.cCompStation> table = myMachine.GetCompensationTable(axName);
        lines.Add("double " + table_var_name + "[" + (table.Count * 7).ToString() + "];");
      }
      lines.Add("\n");

      #endregion

      string t1 = "\t";
      string t2 = "\t\t";
      string t3 = "\t\t\t";

      for (int ii = 0; ii < myMachine.GetAxisNames(false).Length; ii++)
      {
        string scopename = "scope_" + ii.ToString();
        string axName = myMachine.GetAxisNames(false)[ii];
        string table_var_name = axName + "_Table";
        System.Collections.Generic.List<Electroimpact.Machine.cCompTable.cCompStation> comp_table = myMachine.GetCompensationTable(axName);

        lines.Add("\n");
        lines.Add("void " + scopename + "(void)");
        lines.Add("{");

        lines.Add(t1 + "double s" + table_var_name + "[" + (comp_table.Count * 7).ToString() + "] = {");
        for (int jj = 0; jj < comp_table.Count - 1; jj++)
        {
          lines.Add(t3 + comp_table[jj].ToString() + ",");
        }
        lines.Add(t3 + comp_table[comp_table.Count - 1].ToString() + "};");
        lines.Add(t1 + "memcpy(" + table_var_name + ", s" + table_var_name + ", sizeof(" + table_var_name + "));");
        lines.Add("}");
      }

      #region End

      lines.Add("\n");

      lines.Add("void " + "scope_other" + "(void)");
      lines.Add("{");
      for (int ii = 0; ii < myMachine.GetAxisNames(false).Length; ii++)
      {
        string axName = myMachine.GetAxisNames(false)[ii];
        string table_var_name = axName + "_Table";
        int count = myMachine.GetCompensationTable(axName).Count;
        lines.Add(t1 + table_var_name + "_Count = " + count.ToString() + ";");
      }

      lines.Add("\n");

      for (int ii = 0; ii < myMachine.GetAttributeNames().Length; ii++)
      {
        string attrib_name = myMachine.GetAttributeNames()[ii];
        double attrib_value = myMachine.ReadAttribute(attrib_name);
        lines.Add(t1 + attrib_name + " = " + attrib_value.ToString("F6") + ";");
      }


      {
        string attrib_name = "BaseShift_X";
        double attrib_value = myMachine.ReadAttribute(attrib_name);
        lines.Add(t1 + attrib_name + " = " + "0" + ";");//zero because we don't want to machine zero shift to offset coordinate system, work offsets can be used for this.
        attrib_name = "BaseShift_Y";
        attrib_value = myMachine.ReadAttribute(attrib_name);
        lines.Add(t1 + attrib_name + " = " + "0" + ";");
        attrib_name = "BaseShift_Z";
        attrib_value = myMachine.ReadAttribute(attrib_name);
        lines.Add(t1 + attrib_name + " = " + "0" + ";");
        attrib_name = "BaseShift_rX";
        attrib_value = myMachine.ReadAttribute(attrib_name);
        lines.Add(t1 + attrib_name + " = " + attrib_value.ToString("F6") + ";");
        attrib_name = "BaseShift_rY";
        attrib_value = myMachine.ReadAttribute(attrib_name);
        lines.Add(t1 + attrib_name + " = " + attrib_value.ToString("F6") + ";");
        attrib_name = "BaseShift_rZ";
        attrib_value = myMachine.ReadAttribute(attrib_name);
        lines.Add(t1 + attrib_name + " = " + attrib_value.ToString("F6") + ";");
      }

      lines.Add("\n");

      for (int ii = 0; ii < myMachine.GetAxisNames(false).Length; ii++)
      {
        string axis_name = myMachine.GetAxisNames(false)[ii];
        string attrib_name = "scalefactor";
        double attrib_value = myMachine.ReadAxisAttribute(axis_name, attrib_name);
        lines.Add(t1 + axis_name + "_" + attrib_name + " = " + attrib_value.ToString("F6") + ";");
      }

      lines.Add("}");
      #endregion


      System.IO.StreamWriter sw = new StreamWriter(filename);

      for (int ii = 0; ii < lines.Count; ii++)
      {
        sw.WriteLine(lines[ii]);
      }
      sw.Close();
    }


    private void createCBCcodeToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void generateCBCcodeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string output_filename = "";
      myMachine.GenerateCcode(output_filename);
    }

    private void grpSolverControls_Enter(object sender, EventArgs e)
    {

    }
  }
}
