using System;
using Server.Regions;
using System.Collections; using System.Collections.Generic;

namespace Server.Engines.Harvest
{
	public class HarvestBank
	{
		private int m_Current;
		private int m_Maximum;
		private DateTime m_NextRespawn;
		private HarvestVein m_Vein, m_DefaultVein;

		public int GetCurrentFor( Mobile m )
		{
            /*
			HarvestBank fishing = Fishing.System.Definition.GetBank( m.Map, m.X, m.Y );
			CheckRespawn();

			int current = m_Current;

			if ( this == fishing )
				return current;

			if ( m.AccessLevel == AccessLevel.Administrator )
				m.SendMessage( "Current: {0}", current );

			ArrayList list = m.Map.GetSector( m ).Regions;
			for(int i=0;i<list.Count;i++)
			{
				Region r = list[i] as Region;
				if ( r is HouseRegion )
				{
					current -= m_Maximum / 2;
					if ( current < 0 )
						current = 0;
					break;
				}
				else if ( ( r is GuardedRegion && !((GuardedRegion)r).IsDisabled() ) || r is DuelArenaRegion )
				{
					current = 0;
					break;
				}
			}

			if ( m.AccessLevel == AccessLevel.Administrator )
				m.SendMessage( "After check: {0}", current );
            
			return current;*/
            return m_Current;
		}

		public HarvestVein Vein
		{
			get
			{
				CheckRespawn();
				return m_Vein;
			}
			set
			{
				m_Vein = value;
			}
		}

		public HarvestVein DefaultVein
		{
			get
			{
				CheckRespawn();
				return m_DefaultVein;
			}
		}

		public void CheckRespawn()
		{
			if ( m_Current == m_Maximum || m_NextRespawn > DateTime.Now )
				return;

			m_Current = m_Maximum;
			m_Vein = m_DefaultVein;
		}

		public void Consume( HarvestDefinition def, int amount )
		{
			CheckRespawn();

			if ( m_Current == m_Maximum )
			{
				int min = (int)def.MinRespawn.TotalSeconds;
				int max = (int)def.MaxRespawn.TotalSeconds;

				m_Current = m_Maximum - amount;
				m_NextRespawn = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( min, max ) );
			}
			else
			{
				m_Current -= amount;
			}

			if ( m_Current < 0 )
				m_Current = 0;
		}

		public HarvestBank( HarvestDefinition def, HarvestVein defaultVein )
		{
			m_Maximum = Utility.RandomMinMax( def.MinTotal, def.MaxTotal );
			m_Current = m_Maximum;
			m_DefaultVein = defaultVein;
			m_Vein = m_DefaultVein;
		}
	}
}