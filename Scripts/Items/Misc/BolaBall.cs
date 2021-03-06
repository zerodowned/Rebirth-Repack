using System;

namespace Server.Items
{
	public class BolaBall : BaseItem
	{
		[Constructable]
		public BolaBall() : this( 1 )
		{
		}

		[Constructable]
		public BolaBall( int amount ) : base( 0xE73 )
		{
			Weight = 4.0;
			Stackable = true;
			Amount = amount;
		}

		public BolaBall( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new BolaBall( amount ), amount );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}